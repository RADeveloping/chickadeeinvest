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

export default function ColumnOverview() {
    const [searchParams, setSearchParams] = useSearchParams();
    const [properties, errorProperties, loadingProperties] = useFetch('/api/Properties', filterProperties);
    const [units, errorUnits, loadingUnits] = useFetch('/api/Units', filterUnit);
    const [tickets, errorTickets, loadingTickets] = useFetch('/api/Tickets', filterTicket);

    const [selectedPropertyId, setSelectedPropertyId] = useState(null);
    const [selectedUnitId, setSelectedUnitId] = useState(null);
    const [selectedTicketId, setSelectedTicketId] = useState(null);
    const loadingData = loadingProperties || loadingUnits || loadingTickets;

    const [path, setPath] = useState('');
    const [firstLoad, setFirstLoad] = useState(true);
    const isDesktop = useResponsive('up', 'lg');

    const title = "Overview"

    useEffect(() => {
        if (!loadingData) {
            let propertyId = parseInt(searchParams.get('property'))
            let unitId = parseInt(searchParams.get('unit'))
            console.log(unitId)
            if (propertyId) setSelectedPropertyId(propertyId)
            if (unitId) setSelectedUnitId(unitId)
        }
    }, [loadingData])

    useEffect(() => {
        if (selectedPropertyId) {
            let selectedProperty = getItem(properties, selectedPropertyId)
            searchParams.set('property', selectedPropertyId)
            if (!firstLoad) {
                setSelectedUnitId(null)
                searchParams.delete('unit')
            } else {
                setFirstLoad(false)
            }
            setSearchParams(searchParams)
            setPath(`${selectedProperty.dir}`)
        }
    }, [selectedPropertyId])

    useEffect(() => {
        if (selectedUnitId) {
            let selectedProperty = getItem(properties, selectedPropertyId)
            let selectedUnit = getItem(units, selectedUnitId)
            searchParams.set('property', selectedPropertyId)
            searchParams.set('unit', selectedUnitId)
            setSearchParams(searchParams)
            setPath(`${selectedProperty.dir}/Units/${selectedUnit.dir}`)
        }
        setSelectedTicketId(null);
    }, [selectedUnitId])

    const getItem = (items, id) => {
        return items.find(item => item.id === id)
    }

    const viewList = [
        <SimpleList leftRound items={properties} title={"Properties"} setSelectedId={setSelectedPropertyId}
                    selectedId={selectedPropertyId}
                    isDesktop={isDesktop}/>,
        <SimpleList noRound skinny items={selectedPropertyId ?
            units.filter((u) => u.fid === selectedPropertyId) : []}
                    title={"Units"} setNestedSelect={setSelectedPropertyId} path={path}
                    setSelectedId={setSelectedUnitId} selectedId={selectedUnitId}
                    isDesktop={isDesktop}/>,
        <SimpleList rightRound items={selectedUnitId ? tickets.filter((t) => t.fid === selectedUnitId) : []}
                    title={"Tickets"} setNestedSelect={setSelectedUnitId} path={path}
                    setSelectedId={setSelectedTicketId} selectedId={selectedTicketId}
                    isDesktop={isDesktop}/>
    ]

    function getActiveList() {
        if (selectedPropertyId && selectedUnitId) {
            return viewList[2]
        } else if (selectedPropertyId) {
            return viewList[1]
        } else {
            return viewList[0]
        }
    }

    return (
        <>
            <PageLoading loadingData={loadingData}/>
            {!loadingData && isDesktop &&
                <Grow in={!loadingData}>
                    <Stack direction="row">
                        {viewList}
                    </Stack>
                </Grow>
            }
            {!loadingData && !isDesktop &&
                <Grow in={!loadingData}>
                    <Box>
                        {getActiveList()}
                    </Box>
                </Grow>
            }
        </>
    )
}
