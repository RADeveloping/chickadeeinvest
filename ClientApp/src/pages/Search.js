import {
    Box, Button,
    Card,
    Container,
    Grid,
    Grow,
    Stack,
    Typography
} from "@mui/material";
import PageLoading from "../components/PageLoading";
import * as React from "react";
import Page from "../components/Page";
import {ListToolbar} from "../sections/@dashboard/list";
import {useCallback, useEffect, useState} from "react";
import useFetch from "../components/FetchData";
import useResponsive from "../hooks/useResponsive";
import {
    filterProperties, filterTicket, filterUnit,
    propertyProperties,
    ticketProperties,
    unitProperties
} from "../utils/filters";
import Property from "../components/Property";
import useFilter from "../components/FilterOrder";
import SearchRowResult from "../components/SearchRowResult";
import Unit from "../components/Unit";
import Ticket from "../components/Ticket";
import {useNavigate, useSearchParams} from "react-router-dom";
import Iconify from "../components/Iconify";

export default function Search() {
    const title = "Search"
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();

    const [propertySearchParams,
        propertyOrderBy, propertySetOrderBy, propertyHandleOrderByChange,
        propertyOrder, propertySetOrder, propertyHandleOrderChange,
        propertyFilterQuery, handlePropertyFilterByQuery, setPropertyFilterQuery] = useFilter(propertyProperties);

    const [unitSearchParams,
        unitOrderBy, unitSetOrderBy, unitHandleOrderByChange,
        unitOrder, unitSetOrder, unitHandleOrderChange,
        unitFilterQuery, handleUnitFilterByQuery, setUnitFilterQuery] = useFilter(unitProperties);

    const [ticketSearchParams,
        ticketOrderBy, ticketSetOrderBy, ticketHandleOrderByChange,
        ticketOrder, ticketSetOrder, ticketHandleOrderChange,
        ticketFilterQuery, handleTicketFilterByQuery, setTicketFilterQuery] = useFilter(ticketProperties);

    const [properties, errorProperties, loadingProperties] = useFetch(
        propertySearchParams.get('query') ?
            '/api/properties?' + propertySearchParams.toString() : null, filterProperties);
    const [units, errorUnits, loadingUnits] = useFetch(
        unitSearchParams.get('query') ?
            `/api/units?` + unitSearchParams.toString() : null, filterUnit);
    const [tickets, errorTickets, loadingTickets] = useFetch(
        ticketSearchParams.get('query') ?
            `/api/tickets?` + ticketSearchParams.toString() : null, filterTicket);

    const setFilterQueries = (query) => {
        setPropertyFilterQuery(query);
        setUnitFilterQuery(query);
        setTicketFilterQuery(query);
    };
    
    useEffect(()=> {
        let query = searchParams.get('query');
        setFilterQueries(query);
    },[searchParams])
    
    const loadingSearch = loadingUnits || loadingProperties || loadingTickets;
    const isEmptySearch = properties.length === 0 && units.length === 0 && tickets.length === 0;
    
    const showResults = !loadingSearch && !isEmptySearch;
    const showNullResults = !loadingSearch && isEmptySearch;
    const isDesktop = useResponsive('up', 'md');

    return (
        <Page title={title}>
            <Container>
                <Stack direction="column" alignItems="flex-start" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        {title} results for "{searchParams.get('query')}"
                    </Typography>
                    <Button
                        variant="contained"
                        startIcon={<Iconify icon="eva:arrow-back-outline"/>}
                        onClick={() => navigate(-1)}
                    >
                        Back
                    </Button>
                </Stack>
           
                <PageLoading loadingData={loadingSearch} />
                <Grow in={showResults}>
                    <Grid sx={{display: loadingSearch ? 'none' : undefined}} container spacing={2}>
                        <SearchRowResult viewComponent={(data) => <Property navigate={navigate} data={data}/>}
                                         title={"Properties"}
                                         orderBy={propertyOrderBy}
                                         handleOrderByChange={propertyHandleOrderByChange}
                                         properties={propertyProperties}
                                         order={propertyOrder}
                                         handleOrderChange={propertyHandleOrderChange}
                                         isDesktop={isDesktop}
                                         data={properties}
                        />
                        <SearchRowResult viewComponent={(data) => <Unit navigate={navigate} data={data}/>}
                                         title={"Units"}
                                         orderBy={unitOrderBy}
                                         handleOrderByChange={unitHandleOrderByChange}
                                         properties={unitProperties}
                                         order={unitOrder}
                                         handleOrderChange={unitHandleOrderChange}
                                         isDesktop={isDesktop}
                                         data={units}
                        />
                        <SearchRowResult viewComponent={(data) => <Ticket navigate={navigate} data={data}/>}
                                         title={"Tickets"}
                                         orderBy={ticketOrderBy}
                                         handleOrderByChange={ticketHandleOrderByChange}
                                         properties={ticketProperties}
                                         order={ticketOrder}
                                         handleOrderChange={ticketHandleOrderChange}
                                         isDesktop={isDesktop}
                                         data={tickets}
                        />
                    </Grid>
                </Grow>
                {showNullResults &&
                    <Grow in={showNullResults}>
                    <Box sx={{
                        height: '40vh',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        color: 'text.disabled'
                    }}>{`No Results`}</Box>
                    </Grow>}
            </Container>
        </Page>
    )
}