import * as React from 'react';
import {useEffect, useState} from 'react';
import {Box, Grow, Stack} from "@mui/material";
import {useSearchParams} from "react-router-dom";
import useFilter from "../../utils/filter";
import {
    filterProperties, filterTicket,
    filterUnit, getApiTicketsUri, getApiUnitsUri, getTicketsUri,
    PROPERTY,
    TICKET,
    UNIT
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
    const [searchParams, setSearchParams] = useSearchParams();

    const [selectedPropertyId, setSelectedPropertyId] = useState(null);
    const [selectedUnitId, setSelectedUnitId] = useState(null);
    const [selectedTicketId, setSelectedTicketId] = useState(null);

    const [mobileReady, setMobileReady] = useState(true);

    const mobileReadyRefresh = () => {
        setMobileReady(true)
    }

    const [resetOnNull, setResetOnNull] = useState(true)

    const [propertySearchParams,
        propertyOrderBy, propertySetOrderBy, propertyHandleOrderByChange,
        propertyOrder, propertySetOrder, propertyHandleOrderChange] = useFilter(PROPERTY);

    const [unitSearchParams,
        unitOrderBy, unitSetOrderBy, unitHandleOrderByChange,
        unitOrder, unitSetOrder, unitHandleOrderChange] = useFilter(UNIT);

    const [ticketSearchParams,
        ticketOrderBy, ticketSetOrderBy, ticketHandleOrderByChange,
        ticketOrder, ticketSetOrder, ticketHandleOrderChange] = useFilter(TICKET);

    const [properties, errorProperties, loadingProperties] = useFetch('/api/properties?' + propertySearchParams.toString(), filterProperties, undefined,
        mobileReadyRefresh, resetOnNull);
    const [units, errorUnits, loadingUnits] = useFetch(selectedPropertyId ?
            getApiUnitsUri(selectedPropertyId) + '?' + unitSearchParams.toString() : null, filterUnit, undefined,
        mobileReadyRefresh, resetOnNull);
    const [tickets, errorTickets, loadingTickets] = useFetch(selectedUnitId && selectedPropertyId ?
            getApiTicketsUri(selectedPropertyId, selectedUnitId) + '?' + ticketSearchParams.toString() : null, filterTicket, undefined,
        mobileReadyRefresh, resetOnNull);

    const loadingData = loadingProperties || loadingUnits || loadingTickets;
    const [path, setPath] = useState('');
    const [firstLoad, setFirstLoad] = useState(true);
    const isDesktop = useResponsive('up', 'md');

    const getItem = (items, id) => {
        return items.find(item => item.id === id)
    }

    const selectedProperty = getItem(properties, selectedPropertyId)
    const selectedUnit = getItem(units, selectedUnitId)
    
    useEffect(() => {
        let propertyId = searchParams.get('pid')
        let unitId = searchParams.get('uid')
        if (propertyId) setSelectedPropertyId(propertyId)
        if (unitId) setSelectedUnitId(unitId)
    }, [])

    useEffect(() => {
        if (!loadingData) setResetOnNull(false)
    }, [loadingData])

    useEffect(() => {
        let newSearchParams = new URLSearchParams();
        if (selectedPropertyId) newSearchParams.set('pid', selectedPropertyId);
        if (selectedUnitId) newSearchParams.set('uid', selectedUnitId)
        if (!firstLoad) setSearchParams(newSearchParams)
    }, [selectedPropertyId, selectedUnitId])

    useEffect(() => {
        if (selectedPropertyId) {
            if (!firstLoad) {
                setSelectedUnitId(null)
            } else {
                setFirstLoad(false)
            }
        }
    }, [selectedPropertyId])

    useEffect(() => {
        setSelectedTicketId(null);
    }, [selectedUnitId])

    useEffect(() => {
        if (mobileReady) handlePath();
    }, [selectedProperty, selectedUnit, mobileReady])

    const handlePath = () => {
        if (selectedProperty && selectedUnit) {
            setPath(`${selectedProperty.dir}/Units/${selectedUnit.dir}`)
        } else if (selectedProperty) {
            setPath(`${selectedProperty.dir}`)
        }
    }

    const getSelect = (ready, setSelected) => {
        return (val) => {
            setFirstLoad(false);
            setMobileReady(ready)
            setSelected(val)
        }
    }

    const viewList = [
        <SimpleList key={isDesktop ? "sl-1" : undefined} leftRound items={properties} title={"Properties"}
                    setSelectedId={getSelect(false, setSelectedPropertyId)}
                    selectedId={selectedPropertyId}
                    isDesktop={isDesktop} properties={PROPERTY} initialSort={PROPERTY[0].id}
                    loading={loadingProperties}
                    setOrderBy={propertySetOrderBy} order={propertyOrder} setOrder={propertySetOrder}/>,
        <SimpleList key={isDesktop ? "sl-2" : undefined} noRound skinny items={selectedPropertyId ?
            units : []}
                    title={"Units"} setNestedSelect={getSelect(true, setSelectedPropertyId)} path={path}
                    setSelectedId={getSelect(false, setSelectedUnitId)} selectedId={selectedUnitId}
                    isDesktop={isDesktop} properties={UNIT}
                    loading={loadingUnits}
                    setOrderBy={unitSetOrderBy} order={unitOrder} setOrder={unitSetOrder}/>,
        <SimpleList key={isDesktop ? "sl-3" : undefined} rightRound immediateClick items={selectedUnitId ? tickets : []}
                    title={"Tickets"} setNestedSelect={getSelect(true, setSelectedUnitId)} path={path}
                    setSelectedId={setSelectedTicketId} selectedId={selectedTicketId}
                    isDesktop={isDesktop} properties={TICKET}
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
            if (mobileReady) return viewList[2]
            return viewList[1]
        } else if (selectedPropertyId) {
            if (mobileReady) return viewList[1]
            return viewList[0]
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
