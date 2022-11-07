import * as React from 'react';
import {useEffect, useState} from 'react';
import useFetch from "../components/FetchData";
import {Box, Chip, Container, Grid, Grow, Stack, Typography} from "@mui/material";
import Page from "../components/Page";
import SimpleList from "../components/SimpleList";
import PageLoading from "../components/PageLoading";
import useResponsive from "../hooks/useResponsive";
import Label from "../components/Label";
import {filterProperties, filterTicket, filterUnit} from "../utils/filters";
import {useSearchParams} from "react-router-dom";

const propertyProperties = [
    {id: 'propertyId', label: 'Property Id'},
    {id: 'address', label: 'Address'},
    {id: 'unitCount', label: 'Unit Count'},
];

const unitProperties = [
    {id: 'unitId', label: 'Unit Id'},
    {id: 'unitNo', label: 'Unit Number'},
    {id: 'tenantCount', label: 'Tenant Count'},
];


export default function SelectPropertyOverview() {

    const [selectedPropertyId, setSelectedPropertyId] = useState(null);
    const [selectedUnitId, setSelectedUnitId] = useState(null);
    const [selectedTicketId, setSelectedTicketId] = useState(null);

    const [properties, errorProperties, loadingProperties] = useFetch('/api/properties', filterProperties);
    const [units, errorUnits, loadingUnits] = useFetch(selectedPropertyId ?
        `/api/properties/${selectedPropertyId}/units` : null, filterUnit);
    const [tickets, errorTickets, loadingTickets] = useFetch(selectedUnitId && selectedPropertyId ?
        `/api/properties/${selectedPropertyId}/units/${selectedUnitId}/tickets` : null, filterTicket);

    const loadingData = loadingProperties || loadingUnits || loadingTickets;
    const [path, setPath] = useState('');
    const [firstLoad, setFirstLoad] = useState(true);
    const isDesktop = useResponsive('up', 'lg');
    const firstLoadingData = loadingData & firstLoad;


    useEffect(() => {
        if (selectedPropertyId && !loadingProperties) {
            let selectedProperty = getItem(properties, selectedPropertyId)
            if (!selectedProperty) return
            if (!firstLoad) {
                setSelectedUnitId(null)
            } else {
                setFirstLoad(false)
            }
            setPath(`${selectedProperty.dir}`)
        }
    }, [selectedPropertyId, loadingProperties])

    useEffect(() => {
        if (selectedUnitId && !loadingUnits) {
            let selectedProperty = getItem(properties, selectedPropertyId)
            let selectedUnit = getItem(units, selectedUnitId)
            if (!selectedProperty || !selectedUnit) return
            setPath(`${selectedProperty.dir}/Units/${selectedUnit.dir}`)
        }
        setSelectedTicketId(null);
    }, [selectedUnitId, loadingUnits])

    const getItem = (items, id) => {
        return items.find(item => item.id === id)
    }

    const viewList = [
        <SimpleList disableSort items={properties} title={"Properties"} setSelectedId={setSelectedPropertyId}
                    selectedId={selectedPropertyId}
                    isDesktop={isDesktop} properties={propertyProperties} initialSort={propertyProperties[0].id}
                    loading={loadingProperties}/>,
        <SimpleList disableSort noRound skinny items={selectedPropertyId ?
            units : []}
                    title={"Units"} setNestedSelect={setSelectedPropertyId} path={path}
                    setSelectedId={setSelectedUnitId} selectedId={selectedUnitId}
                    isDesktop={isDesktop} properties={unitProperties}
                    loading={loadingUnits}/>,
      
    ]

    function getActiveList() {
       if (selectedPropertyId) {
            return viewList[1]
        } else {
            return viewList[0]
        }
    }

    return (
        <>
            <PageLoading loadingData={firstLoadingData}/>
            {!firstLoadingData &&
                <Grow in={!firstLoadingData}>
                    <Box>
                        {getActiveList()}
                    </Box>
                </Grow>
            }
        </>
    )
}
