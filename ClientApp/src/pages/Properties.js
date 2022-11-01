import {
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
import {Link as RouterLink, useNavigate} from "react-router-dom";
import Iconify from "../components/Iconify";
import PageLoading from "../components/PageLoading";
import * as React from "react";
import Page from "../components/Page";
import {applySortFilter, getComparator, ListToolbar} from "../sections/@dashboard/list";
import {useState} from "react";
import useFetch from "../components/FetchData";
import useResponsive from "../hooks/useResponsive";
import {getUnitBox} from "../utils/filters";
import Label from "../components/Label";

const properties = [
    {id: 'address', label: 'Address'},
    {id: 'propertyId', label: 'Property Id'},
    {id: 'openTicketCount', label: 'Open Ticket Count'},
    {id: 'unitCount', label: 'Unit Count'},
    {id: 'propertyManagerName', label: 'Property Manager Name'},
];

export default function Properties() {
    const filterData = (data) => {
        data.forEach((d) => {
            let openTicketCount = 0;
            d.units.forEach((unit) => {
                for (let i = 0; i < unit.tickets.length; i++) {
                    if (unit.tickets[i].status === 0) {
                        openTicketCount++
                    }
                }
            })
            d.openTicketCount = openTicketCount
            d.unitCount = d.units.length
            d.propertyManagerName = `${d.propertyManager.firstName} ${d.propertyManager.lastName}`
        })
        return data;
    }
    const navigate = useNavigate();
    const title = "Properties"
    const dataName = 'Property';
    const [filterQueryProperty, setFilterQueryProperty] = useState('address')
    const [orderBy, setOrderBy] = useState('openTicketCount');
    const [data, errorData, loadingData] = useFetch('/api/Properties', filterData);
    const [order, setOrder] = useState('desc');
    const [filterQuery, setFilterQuery] = useState('');

    const handleFilterByQuery = (event) => {
        setFilterQuery(event.target.value);
    };

    const handleOrderByChange = (event) => {
        setOrderBy(event.target.value);
    };

    const filteredData = applySortFilter(data, getComparator(order, orderBy), filterQuery, filterQueryProperty);
    const isDesktop = useResponsive('up', 'sm');
    const isDataNotFound = filteredData.length === 0 && data.length > 0;

    const noData = data.length === 0;


    console.log(filteredData)
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
                <PageLoading loadingData={loadingData}/>
                <Grow in={!loadingData}>
                    <Stack direction={isDesktop ? 'row' : 'column'} gap={1} alignItems={'center'} justifyContent={'space-between'}>
                        <Card sx={{
                            boxShadow: 'rgba(149, 157, 165, 0.2) 0px 8px 24px',
                            display: loadingData ? 'none' : undefined,
                            width: 'fit-content',
                            backgroundColor: (theme) => theme.palette['background'].default,
                        }}>

                            <ListToolbar
                                filterQuery={filterQuery}
                                onFilterQuery={handleFilterByQuery}
                                properties={properties}
                                filterQueryProperty={filterQueryProperty}
                                setFilterQueryProperty={setFilterQueryProperty}
                                setFilterQuery={setFilterQuery}
                            />

                        </Card>
                        <Card sx={{
                            boxShadow: 'rgba(149, 157, 165, 0.2) 0px 8px 24px',
                            display: loadingData ? 'none' : undefined,
                            width: 'fit-content',
                            backgroundColor: (theme) => theme.palette['background'].default,
                            padding: 2.5
                        }}>
                            <FormControl sx={{minWidth: 80}}>
                                <InputLabel id="sort-property">Order by</InputLabel>
                                <Select
                                    labelId="sort-property"
                                    value={orderBy}
                                    label="Order by"
                                    onChange={handleOrderByChange}
                                >
                                    {properties.map((p) =>
                                        <MenuItem key={p.id} value={p.id}>{p.label}</MenuItem>
                                    )}
                                </Select>
                            </FormControl>
                        </Card>
                    </Stack>
                </Grow>
                <br/>
                <Grow in={!loadingData && filteredData.length > 0}>
                    <Grid container spacing={1}>
                        {filteredData.map((data) => {
                                const {address, propertyId, openTicketCount, unitCount, propertyManagerName} = data
                                return (<Grow in={true}>
                                    <Grid xs={6} sm={6} md={6} l={4} xl={4} item>
                                        <Card sx={{height: 200}}>
                                            <CardContent>
                                                <Stack direction={'column'} justifyContent={'center'}>
                                                    <Typography variant={'h4'}>
                                                        {address}
                                                    </Typography>
                                                    <Stack direction={'row'} alignItems={'center'} gap={1}>
                                                        <Label>
                                                            {unitCount} unit{unitCount !== 1 && 's'}
                                                        </Label>
                                                        <Label color={'info'}>
                                                            {openTicketCount} open ticket{openTicketCount !== 1 && 's'}
                                                        </Label>
                                                    </Stack>
                                                    <br/>
                                                    <Grid container spacing={4} alignItems={'center'}
                                                          justifyContent={'space-between'}>
                                                        <Grid item>
                                                            <Typography sx={{fontSize: 14}} color="text.secondary"
                                                                        gutterBottom>
                                                                Property Manager
                                                            </Typography>
                                                            <Typography variant={'h6'} sx={{fontWeight: 'normal'}}>
                                                                {propertyManagerName}
                                                            </Typography>
                                                        </Grid>
                                                        <Grid item>
                                                            <Typography sx={{fontSize: 14}} color="text.secondary"
                                                                        gutterBottom>
                                                                Id
                                                            </Typography>
                                                            <Typography variant={'h6'} sx={{fontWeight: 'normal'}}>
                                                                {propertyId}
                                                            </Typography>
                                                        </Grid>
                                                    </Grid>
                                                </Stack>
                                            </CardContent>
                                        </Card>
                                    </Grid>
                                </Grow>)
                            }
                        )}
                    </Grid>
                </Grow>
            </Container>
        </Page>
    )
}