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
    getPropertiesUri,
    getUnitBox,
    propertyProperties,
    ticketProperties,
    unitProperties
} from "../utils/filters";
import Label from "../components/Label";
import {ToggleButton, ToggleButtonGroup} from "@mui/lab";
import Property from "../components/Property";
import useFilter from "../components/FilterOrder";

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
        '/api/properties?' + propertySearchParams.toString(), filterProperties);
    const [units, errorUnits, loadingUnits] = useFetch(
        `/api/units?` + unitSearchParams.toString(), filterUnit);
    const [tickets, errorTickets, loadingTickets] = useFetch(
        `/api/tickets?` + ticketSearchParams.toString(), filterTicket);

    const loadingSearch = loadingProperties || loadingUnits || loadingTickets;
    
    useEffect(()=> {
        setUnitFilterQuery(propertyFilterQuery);
        setTicketUnitQuery(propertyFilterQuery);
    },[propertyFilterQuery])

    const isDesktop = useResponsive('up', 'md');

    return (
        <Page title={title}>
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        {title}
                    </Typography>
                    <Button
                        variant="contained"
                        component={RouterLink}
                        to="#"
                        startIcon={<Iconify icon="eva:plus-fill"/>}
                    >
                        {`New ${dataName}`}
                    </Button>
                </Stack>
                <PageLoading loadingData={loadingSearch}/>
                <Grow in={!loadingSearch}>
                    <Stack direction={isDesktop ? 'row' : 'column'} gap={1} alignItems={'center'}
                           justifyContent={'space-between'}>
                        <Card sx={{
                            boxShadow: 'rgba(149, 157, 165, 0.2) 0px 8px 24px',
                            display: loadingSearch ? 'none' : undefined,
                            width: 'fit-content',
                            backgroundColor: (theme) => theme.palette['background'].default,
                        }}>

                            <ListToolbar
                                filterQuery={propertyFilterQuery}
                                onFilterQuery={handlePropertyFilterByQuery}
                                properties={propertyProperties}
                                setFilterQuery={setPropertyFilterQuery}
                            />

                        </Card>
                        <Card sx={{
                            boxShadow: 'rgba(149, 157, 165, 0.2) 0px 8px 24px',
                            display: loadingSearch ? 'none' : undefined,
                            width: 'fit-content',
                            backgroundColor: (theme) => theme.palette['background'].default,
                            padding: 2.5
                        }}>
                            <Stack direction={'row'} alignItems={'stretch'} gap={1}>
                                <FormControl sx={{minWidth: 80}}>
                                    <InputLabel id="sort-property">Order by</InputLabel>
                                    <Select
                                        labelId="sort-property"
                                        value={propertyOrderBy}
                                        label="Order by"
                                        onChange={propertyHandleOrderByChange}
                                    >
                                        {propertyProperties.map((p) =>
                                            <MenuItem key={p.id} value={p.id}>{p.label}</MenuItem>
                                        )}
                                    </Select>
                                </FormControl>
                                <ToggleButtonGroup
                                    color="primary"
                                    value={propertyOrder}
                                    exclusive
                                    onChange={propertyHandleOrderChange}
                                >
                                    <ToggleButton value="desc"><Iconify sx={{height: 14, width: 'auto'}}
                                                                        icon="cil:sort-ascending"/></ToggleButton>
                                    <ToggleButton value="asc"><Iconify sx={{height: 14, width: 'auto'}}
                                                                       icon="cil:sort-descending"/></ToggleButton>
                                </ToggleButtonGroup>
                            </Stack>
                        </Card>
                    </Stack>
                </Grow>
                <br/>
                <Grow in={!loadingSearch && properties.length > 0}>
                    <Grid container spacing={1} justifyContent={isDesktop ? undefined : 'center'}>
                        {properties.map((data) =>
                                // <Link to={'/dashboard/' + getPropertiesUri(data)} style={{textDecoration: 'none'}}>
                                <Property data={data}/>
                            // </Link>
                        )}
                    </Grid>
                </Grow>
                {!loadingSearch && properties.length === 0 &&
                    <Box sx={{
                        height: '40vh',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        color: 'text.disabled'
                    }}>{`No ${title}`}</Box>}
            </Container>
        </Page>
    )
}