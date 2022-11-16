import * as React from 'react';
import {useEffect, useState} from 'react';
import {Box, Grow, Stack} from "@mui/material";
import {useSearchParams} from "react-router-dom";
import useFilter from "../../utils/filter";
import {
    filterProperties, filterTicket,
    filterUnit, getPropertiesUri, getTicketsUri, getUnitsUri,
    propertyProperties,
    ticketProperties,
    unitProperties
} from "../../utils/constants";
import useFetch from "../../utils/fetch";
import SimpleList from "./SimpleList";
import AddTicket from "../overlay/AddTicket";
import PageLoading from "../common/PageLoading";
import useResponsive from "../../utils/responsive";


/**
 * Component column view for Tickets page.
 * @returns {JSX.Element}
 * @constructor
 */
export default function ColumnOverview() {
    // mobileWait uses these constants to delay the transition from column views while loading in mobile view.
    const WAIT = -2
    const BACK = -1
    const READY = 0

    const [searchParams, setSearchParams] = useSearchParams();

    const [selectedPropertyId, setSelectedPropertyId] = useState(null);
    const [selectedUnitId, setSelectedUnitId] = useState(null);
    const [selectedTicketId, setSelectedTicketId] = useState(null);

    const [mobileWait, setMobileWait] = useState(-1);

    const mobileWaitRefresh = () => {
        if (mobileWait === WAIT) setMobileWait(READY)
    }

    const [propertySearchParams,
        propertyOrderBy, propertySetOrderBy, propertyHandleOrderByChange,
        propertyOrder, propertySetOrder, propertyHandleOrderChange] = useFilter(propertyProperties);

    const [unitSearchParams,
        unitOrderBy, unitSetOrderBy, unitHandleOrderByChange,
        unitOrder, unitSetOrder, unitHandleOrderChange] = useFilter(unitProperties);

    const [ticketSearchParams,
        ticketOrderBy, ticketSetOrderBy, ticketHandleOrderByChange,
        ticketOrder, ticketSetOrder, ticketHandleOrderChange] = useFilter(ticketProperties);

    const [properties, errorProperties, loadingProperties] = useFetch('/api/properties?' + propertySearchParams.toString(), filterProperties, undefined,
        mobileWaitRefresh);
    const [units, errorUnits, loadingUnits] = useFetch(selectedPropertyId ?
        `/api/properties/${selectedPropertyId}/units?` + unitSearchParams.toString() : null, filterUnit, undefined, 
        mobileWaitRefresh);
    const [tickets, errorTickets, loadingTickets] = useFetch(selectedUnitId && selectedPropertyId ?
        `/api/properties/${selectedPropertyId}/units/${selectedUnitId}/tickets?` + ticketSearchParams.toString() : null, filterTicket, undefined,
        mobileWaitRefresh);

    const loadingData = loadingProperties || loadingUnits || loadingTickets;
    const [path, setPath] = useState('');
    const [firstLoad, setFirstLoad] = useState(true);
    const isDesktop = useResponsive('up', 'lg');
    const firstLoadingData = loadingData & firstLoad;

    const getItem = (items, id) => {
        return items.find(item => item.id === id)
    }

    const selectedProperty = getItem(properties, selectedPropertyId)
    const selectedUnit = getItem(units, selectedUnitId)

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
            searchParams.set('property', selectedPropertyId)
            if (!firstLoad) {
                setSelectedUnitId(null)
                searchParams.delete('unit')
                setSearchParams(searchParams)
            } else {
                setFirstLoad(false)
            }
        } else if (!selectedPropertyId) {
            if (!firstLoad) {
                searchParams.delete('property');
                setSearchParams(searchParams)
            }
        }
    }, [selectedPropertyId, loadingProperties])

    useEffect(() => {
        if (selectedUnitId && !loadingUnits) {
            searchParams.set('property', selectedPropertyId)
            searchParams.set('unit', selectedUnitId)
            if (!firstLoad) setSearchParams(searchParams)
        } else if (!selectedUnitId && selectedPropertyId) {
            searchParams.set('property', selectedPropertyId)
            searchParams.delete('unit')
            if (!firstLoad) setSearchParams(searchParams)
        }
        setSelectedTicketId(null);
    }, [selectedUnitId, loadingUnits])

    useEffect(() => {
        handlePath();
    }, [selectedProperty, selectedUnit])

    const handlePath = () => {
        if (selectedProperty && selectedUnit) {
            setPath(`${selectedProperty.dir}/Units/${selectedUnit.dir}`)
        } else if (selectedProperty) {
            setPath(`${selectedProperty.dir}`)
        }
    }

    const getSelect = (mobileVal, setSelected) => {
        return (val) => {
            setFirstLoad(false);
            setMobileWait(mobileVal)
            setSelected(val)
        }
    }

    const viewList = [
        <SimpleList key={isDesktop ? "sl-1" : undefined} leftRound items={properties} title={"Properties"}
                    setSelectedId={getSelect(-2, setSelectedPropertyId)}
                    selectedId={selectedPropertyId}
                    isDesktop={isDesktop} properties={propertyProperties} initialSort={propertyProperties[0].id}
                    loading={loadingProperties} uri={getPropertiesUri}
                    setOrderBy={propertySetOrderBy} order={propertyOrder} setOrder={propertySetOrder}/>,
        <SimpleList key={isDesktop ? "sl-2" : undefined} noRound skinny items={selectedPropertyId ?
            units : []}
                    title={"Units"} setNestedSelect={getSelect(-1, setSelectedPropertyId)} path={path}
                    setSelectedId={getSelect(-2, setSelectedUnitId)} selectedId={selectedUnitId}
                    isDesktop={isDesktop} properties={unitProperties}
                    loading={loadingUnits} uri={getUnitsUri}
                    setOrderBy={unitSetOrderBy} order={unitOrder} setOrder={unitSetOrder}/>,
        <SimpleList key={isDesktop ? "sl-3" : undefined} rightRound immediateClick items={selectedUnitId ? tickets : []}
                    title={"Tickets"} setNestedSelect={getSelect(-1, setSelectedUnitId)} path={path}
                    setSelectedId={setSelectedTicketId} selectedId={selectedTicketId}
                    isDesktop={isDesktop} properties={ticketProperties}
                    loading={loadingTickets} uri={getTicketsUri}
                    setOrderBy={ticketSetOrderBy} order={ticketOrder} setOrder={ticketSetOrder}
                    addComponent={selectedUnitId && selectedPropertyId ?
                        (open, handleClose) => <AddTicket
                            title={units.length > 0 && selectedUnitId ? getItem(units, selectedUnitId).unitNo : null}
                            unitId={selectedUnitId} propertyId={selectedPropertyId}
                            open={open} handleClose={handleClose}/>
                        : undefined
                    }
        />
    ]

    function getActiveList() {
        if (selectedPropertyId && selectedUnitId) {
            if (mobileWait === BACK || mobileWait === READY) return viewList[2]
            return viewList[1]
        } else if (selectedPropertyId) {
            if (mobileWait === BACK || mobileWait === READY) return viewList[1]
            return viewList[0]
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
