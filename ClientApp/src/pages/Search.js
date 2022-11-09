import {
    Box,
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

export default function Search() {
    const title = "Search"

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
        ticketFilterQuery, handleTicketFilterByQuery, setTicketUnitQuery] = useFilter(ticketProperties);

    const [properties, errorProperties, loadingProperties] = useFetch(
        propertyFilterQuery ?
            '/api/properties?' + propertySearchParams.toString() : null, filterProperties);
    const [units, errorUnits, loadingUnits] = useFetch(
        propertyFilterQuery ?
            `/api/units?` + unitSearchParams.toString() : null, filterUnit);
    const [tickets, errorTickets, loadingTickets] = useFetch(
        propertyFilterQuery ?
            `/api/tickets?` + ticketSearchParams.toString() : null, filterTicket);

    const [mainFilterQuery, setMainFilterQuery] = useState('');

    const setFilterQueries = (query) => {
        setPropertyFilterQuery(query);
        setUnitFilterQuery(query);
        setTicketUnitQuery(query);
    };
    
    useEffect(()=> {
        setFilterQueries(mainFilterQuery);
    }, [mainFilterQuery])

    const loadingSearch = loadingProperties || loadingUnits || loadingTickets;
    const isEmptySearch = properties.length === 0 && units.length === 0 && tickets.length === 0;

    const showResults = !loadingSearch && !isEmptySearch;
    const showNullResults = !loadingSearch && isEmptySearch;

    useEffect(() => {
        setPropertyFilterQuery(mainFilterQuery);
        setUnitFilterQuery(mainFilterQuery);
        setTicketUnitQuery(mainFilterQuery);
    }, [mainFilterQuery])


    const isDesktop = useResponsive('up', 'md');

    return (
        <Page title={title}>
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        {title}
                    </Typography>
                </Stack>
                <PageLoading loadingData={loadingSearch}/>
                <Grow in={!loadingSearch}>
                    <Stack direction={isDesktop ? 'row' : 'column'} gap={1} alignItems={'center'}
                           justifyContent={'space-between'}>
                        <Card sx={{
                            boxShadow: 'rgba(149, 157, 165, 0.2) 0px 8px 24px',
                            display: loadingSearch ? 'none' : undefined,
                            width: isDesktop ? 'fit-content' : '100%',
                            backgroundColor: (theme) => theme.palette['background'].default,
                        }}>

                            <ListToolbar
                                isDesktop={isDesktop}
                                filterQuery={mainFilterQuery}
                                setFilterQuery={setMainFilterQuery}
                            />

                        </Card>
                    </Stack>
                </Grow>
                <br/>
                <Grow in={showResults}>
                    <Grid container spacing={2}>
                        {properties.length !== 0 &&
                            <SearchRowResult viewComponent={(data) => <Property data={data}/>}
                                             title={"Properties"}
                                             loadingSearch={loadingSearch}
                                             orderBy={propertyOrderBy}
                                             handleOrderByChange={propertyHandleOrderByChange}
                                             properties={propertyProperties}
                                             order={propertyOrder}
                                             handleOrderChange={propertyHandleOrderChange}
                                             isDesktop={isDesktop}
                                             data={properties}
                            />
                        }
                        {units.length !== 0 &&
                            <SearchRowResult viewComponent={(data) => <Unit data={data}/>}
                                             title={"Units"} loadingSearch={loadingSearch}
                                             orderBy={unitOrderBy}
                                             handleOrderByChange={unitHandleOrderByChange}
                                             properties={unitProperties}
                                             order={unitOrder}
                                             handleOrderChange={unitHandleOrderChange}
                                             isDesktop={isDesktop}
                                             data={units}
                            />
                        }
                        {tickets.length !== 0 &&
                            <SearchRowResult viewComponent={(data) => <Ticket data={data}/>}
                                             title={"Tickets"} loadingSearch={loadingSearch}
                                             orderBy={ticketOrderBy}
                                             handleOrderByChange={ticketHandleOrderByChange}
                                             properties={ticketProperties}
                                             order={ticketOrder}
                                             handleOrderChange={ticketHandleOrderChange}
                                             isDesktop={isDesktop}
                                             data={tickets}
                            />
                        }
                    </Grid>
                </Grow>
                {showNullResults &&
                    <Box sx={{
                        height: '40vh',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        color: 'text.disabled'
                    }}>{`No ${propertyFilterQuery ? 'Results' : 'Search'}`}</Box>}
            </Container>
        </Page>
    )
}