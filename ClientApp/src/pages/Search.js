import {
    Box, Button,
    Card, CircularProgress,
    Container,
    Grid,
    Grow,
    Stack,
    Typography
} from "@mui/material";
import * as React from "react";
import Page from "../components/common/Page";
import {useEffect} from "react";
import useFetch from "../utils/fetch";
import {
    filterProperties, filterTicket, filterUnit,
    PROPERTY,
    TICKET,
    UNIT
} from "../utils/constants";
import Property from "../components/search/Property";
import useFilter from "../utils/filter";
import SearchRowResult from "../components/search/SearchRowResult";
import Unit from "../components/search/Unit";
import Ticket from "../components/search/Ticket";
import {useNavigate, useSearchParams} from "react-router-dom";
import Iconify from "../components/common/Iconify";
import useResponsive from "../utils/responsive";

export default function Search() {
    const title = "Search"
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();

    const [propertySearchParams,
        propertyOrderBy, propertySetOrderBy, propertyHandleOrderByChange,
        propertyOrder, propertySetOrder, propertyHandleOrderChange,
        propertyFilterQuery, handlePropertyFilterByQuery, setPropertyFilterQuery] = useFilter(PROPERTY);

    const [unitSearchParams,
        unitOrderBy, unitSetOrderBy, unitHandleOrderByChange,
        unitOrder, unitSetOrder, unitHandleOrderChange,
        unitFilterQuery, handleUnitFilterByQuery, setUnitFilterQuery] = useFilter(UNIT);

    const [ticketSearchParams,
        ticketOrderBy, ticketSetOrderBy, ticketHandleOrderByChange,
        ticketOrder, ticketSetOrder, ticketHandleOrderChange,
        ticketFilterQuery, handleTicketFilterByQuery, setTicketFilterQuery] = useFilter(TICKET);

    const [properties, errorProperties, loadingProperties] = useFetch(
        propertySearchParams.get('query') ?
            '/api/properties?' + propertySearchParams.toString() : null, filterProperties, true);
    const [units, errorUnits, loadingUnits] = useFetch(
        unitSearchParams.get('query') ?
            `/api/units?` + unitSearchParams.toString() : null, filterUnit, true);
    const [tickets, errorTickets, loadingTickets] = useFetch(
        ticketSearchParams.get('query') ?
            `/api/tickets?` + ticketSearchParams.toString() : null, filterTicket, true);

    const setFilterQueries = (query) => {
        setPropertyFilterQuery(query);
        setUnitFilterQuery(query);
        setTicketFilterQuery(query);
    };

    useEffect(() => {
        let query = searchParams.get('query');
        setFilterQueries(query);
    }, [searchParams])

    const loadingSearch = loadingUnits || loadingProperties || loadingTickets;
    const isEmptySearch = properties.length === 0 && units.length === 0 && tickets.length === 0;

    const showResults = !isEmptySearch;
    const showNullResults = isEmptySearch;
    const isDesktop = useResponsive('up', 'md');

    return (
        <Page title={title}>
            <Container>
                <Stack direction="column" alignItems="flex-start" justifyContent="space-between" mb={5}>
                    <Stack direction="row" alignItems={'center'} justifyContent="space-between" width={'100%'}>
                        <Typography variant="h4" gutterBottom>
                            {title} results for "{searchParams.get('query')}"
                        </Typography>
                        <Grow timeout={{enter: 3000}} in={loadingSearch}><CircularProgress size={30}/></Grow>
                    </Stack>
                    <Button
                        variant="contained"
                        startIcon={<Iconify icon="eva:arrow-back-outline"/>}
                        onClick={() => navigate(-1)}
                    >
                        Back
                    </Button>
                </Stack>
                <Grow in={showResults}>
                    <Grid container spacing={2}>
                        <SearchRowResult key={'prop-search'}
                                         viewComponent={(data) => <Property key={data.propertyId} navigate={navigate}
                                                                            data={data}/>}
                                         title={"Properties"}
                                         orderBy={propertyOrderBy}
                                         handleOrderByChange={propertyHandleOrderByChange}
                                         properties={PROPERTY}
                                         order={propertyOrder}
                                         handleOrderChange={propertyHandleOrderChange}
                                         isDesktop={isDesktop}
                                         data={properties}
                        />
                        <SearchRowResult key={'unit-search'}
                                         viewComponent={(data) => <Unit key={data.unitId} navigate={navigate}
                                                                        data={data}/>}
                                         title={"Units"}
                                         orderBy={unitOrderBy}
                                         handleOrderByChange={unitHandleOrderByChange}
                                         properties={UNIT}
                                         order={unitOrder}
                                         handleOrderChange={unitHandleOrderChange}
                                         isDesktop={isDesktop}
                                         data={units}
                        />
                        <SearchRowResult key={'ticket-search'}
                                         viewComponent={(data) => <Ticket key={data.ticketId} navigate={navigate}
                                                                          data={data}/>}
                                         title={"Tickets"}
                                         orderBy={ticketOrderBy}
                                         handleOrderByChange={ticketHandleOrderByChange}
                                         properties={TICKET}
                                         order={ticketOrder}
                                         handleOrderChange={ticketHandleOrderChange}
                                         isDesktop={isDesktop}
                                         data={tickets}
                        />
                    </Grid>
                </Grow>
                {showNullResults && !loadingSearch &&
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