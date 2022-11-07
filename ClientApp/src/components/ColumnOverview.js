import * as React from 'react';
import {useEffect, useState} from 'react';
import useFetch from "../components/FetchData";
import {Box, Grow, Stack} from "@mui/material";
import SimpleList from "../components/SimpleList";
import PageLoading from "../components/PageLoading";
import useResponsive from "../hooks/useResponsive";
import {filterProperties, filterUnit} from "../utils/filters";
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

export default function ColumnOverview() {
    const [searchParams, setSearchParams] = useSearchParams();

    const [selectedPropertyId, setSelectedPropertyId] = useState(null);
    const [selectedUnitId, setSelectedUnitId] = useState(null);

    const [properties, errorProperties, loadingProperties] = useFetch('/api/properties', filterProperties);
    const [units, errorUnits, loadingUnits] = useFetch(selectedPropertyId ? 
        `/api/properties/${selectedPropertyId}/units` : null, filterUnit);

    const loadingData = loadingProperties || loadingUnits;
    const [path, setPath] = useState('');
    const [firstLoad, setFirstLoad] = useState(true);
    const isDesktop = useResponsive('up', 'lg');
    const firstLoadingData = loadingData & firstLoad;

    useEffect(() => {
        if (!loadingData) {
            let propertyId = searchParams.get('property')
            let unitId = searchParams.get('unit')
            if (propertyId) setSelectedPropertyId(propertyId)
            if (unitId) setSelectedUnitId(unitId)
        }
    }, [loadingData])

    useEffect(() => {
        if (selectedPropertyId && !loadingProperties) {
            let selectedProperty = getItem(properties, selectedPropertyId)
            if (!selectedProperty) return
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
    }, [selectedPropertyId, loadingProperties])

    useEffect(() => {
        if (selectedUnitId && !loadingUnits) {
            let selectedProperty = getItem(properties, selectedPropertyId)
            let selectedUnit = getItem(units, selectedUnitId)
            if (!selectedProperty || !selectedUnit) return
            searchParams.set('property', selectedPropertyId)
            searchParams.set('unit', selectedUnitId)
            setSearchParams(searchParams)
            setPath(`${selectedProperty.dir}/Units/${selectedUnit.dir}`)
        }
    }, [selectedUnitId, loadingUnits])

    const getItem = (items, id) => {
        return items.find(item => item.id === id)
    }

    const viewList = [
        <SimpleList leftRound items={properties} title={"Properties"} setSelectedId={setSelectedPropertyId}
                    selectedId={selectedPropertyId}
                    isDesktop={isDesktop} properties={propertyProperties} initialSort={propertyProperties[0].id}
                    loading={loadingProperties}/>,
        <SimpleList noRound skinny items={selectedPropertyId ?
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
            {!firstLoadingData && isDesktop &&
                <Grow in={!firstLoadingData}>
                    <Stack direction="row">
                        {viewList}
                    </Stack>
                </Grow>
            }
            {!firstLoadingData && !isDesktop &&
                <Grow in={!firstLoadingData}>
                    <Box>
                        {getActiveList()}
                    </Box>
                </Grow>
            }
        </>
    )
}
