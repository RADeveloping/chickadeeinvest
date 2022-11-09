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
import {debounce} from "lodash";

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

    const debouncedFilterQueries = useCallback(debounce(query =>
        setFilterQueries(query), 500), []
    )

    debouncedFilterQueries(mainFilterQuery);
    
    const isEmptySearch = properties.length === 0 && units.length === 0 && tickets.length === 0;

    const isDesktop = useResponsive('up', 'md');

    return (
        <Page title={title}>
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        {title}
                    </Typography>
                </Stack>
                <Grow in={true}>
                    <Stack direction={isDesktop ? 'row' : 'column'} gap={1} alignItems={'center'}
                           justifyContent={'space-between'}>
                        <Card sx={{
                            boxShadow: 'rgba(149, 157, 165, 0.2) 0px 8px 24px',
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
                <Grow in={!isEmptySearch}>
                    <Grid container spacing={2}>
                        <SearchRowResult viewComponent={(data) => <Property data={data}/>}
                                         title={"Properties"}
                                         orderBy={propertyOrderBy}
                                         handleOrderByChange={propertyHandleOrderByChange}
                                         properties={propertyProperties}
                                         order={propertyOrder}
                                         handleOrderChange={propertyHandleOrderChange}
                                         isDesktop={isDesktop}
                                         data={properties}
                        />
                        <SearchRowResult viewComponent={(data) => <Unit data={data}/>}
                                         title={"Units"}
                                         orderBy={unitOrderBy}
                                         handleOrderByChange={unitHandleOrderByChange}
                                         properties={unitProperties}
                                         order={unitOrder}
                                         handleOrderChange={unitHandleOrderChange}
                                         isDesktop={isDesktop}
                                         data={units}
                        />
                        <SearchRowResult viewComponent={(data) => <Ticket data={data}/>}
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
                {isEmptySearch &&
                    <Box sx={{
                        height: '40vh',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        color: 'text.disabled'
                    }}>{`No Results`}</Box>}
            </Container>
        </Page>
    )
}