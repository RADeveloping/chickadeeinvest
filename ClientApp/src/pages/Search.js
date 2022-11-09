import {
    Box,
    Button,
    Card,
    CardContent,
    Container,
    FormControl,
    Grid,
    Grow, InputLabel,
    ListItemButton, MenuItem, Select,
    Stack,
    Typography
} from "@mui/material";
import {Link, Link as RouterLink, useNavigate} from "react-router-dom";
import Iconify from "../components/Iconify";
import PageLoading from "../components/PageLoading";
import * as React from "react";
import Page from "../components/Page";
import {applySortFilter, getComparator, ListToolbar} from "../sections/@dashboard/list";
import {useEffect, useState} from "react";
import useFetch from "../components/FetchData";
import useResponsive from "../hooks/useResponsive";
import {
    filterProperties, filterTicket, filterUnit,
    getPropertiesUri, getTicketBox,
    getUnitBox,
    propertyProperties,
    ticketProperties,
    unitProperties
} from "../utils/filters";
import Label from "../components/Label";
import {ToggleButton, ToggleButtonGroup} from "@mui/lab";
import Property from "../components/Property";
import useFilter from "../components/FilterOrder";
import SortControl from "../components/SortControl";
import SearchRowResult from "../components/SearchRowResult";
import Unit from "../components/Unit";
import Ticket from "../components/Ticket";

export default function Search() {
    const title = "Search"
    const dataName = 'wtf';

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

    const loadingSearch = loadingProperties || loadingUnits || loadingTickets;
    const isEmptySearch = properties.length === 0 && units.length === 0 && tickets.length === 0;

    useEffect(() => {
        setUnitFilterQuery(propertyFilterQuery);
        setTicketUnitQuery(propertyFilterQuery);
    }, [propertyFilterQuery])

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
                                filterQuery={propertyFilterQuery}
                                onFilterQuery={handlePropertyFilterByQuery}
                                setFilterQuery={setPropertyFilterQuery}
                            />

                        </Card>
                    </Stack>
                </Grow>
                <br/>
                <Grow in={!loadingSearch && (properties.length !== 0 || units.length !== 0 || tickets.length !== 0)}>
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
                {!loadingSearch && isEmptySearch &&
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