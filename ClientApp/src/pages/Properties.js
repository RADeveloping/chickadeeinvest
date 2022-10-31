import {Button, Card, Container, Grow, Stack, Typography} from "@mui/material";
import {Link as RouterLink, useNavigate} from "react-router-dom";
import Iconify from "../components/Iconify";
import PageLoading from "../components/PageLoading";
import * as React from "react";
import Page from "../components/Page";
import {applySortFilter, getComparator, ListToolbar} from "../sections/@dashboard/list";
import {useState} from "react";
import useFetch from "../components/FetchData";

const properties = [
    { id: 'address', label: 'Address'},
    { id: 'propertyId', label: 'Id'},
];

export default function Properties() {
    const navigate = useNavigate();
    const title = "Properties"
    const dataName = 'Property';
    const dataId = 'propertyId';
    const [filterQueryProperty, setFilterQueryProperty] = useState('property')
    const [orderBy, setOrderBy] = useState('status');
    const [data, errorData, loadingData] = useFetch('/api/Properties');
    const [order, setOrder] = useState('asc');
    const [filterQuery, setFilterQuery] = useState('');
    
    const handleFilterByQuery = (event) => {
        setFilterQuery(event.target.value);
    };

    const filteredData = applySortFilter(data, getComparator(order, orderBy), filterQuery, filterQueryProperty);

    const isDataNotFound = filteredData.length === 0 && data.length > 0;

    const noData = data.length === 0;
    
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
                        startIcon={<Iconify icon="eva:plus-fill" />}
                    >
                        {`New ${dataName}`}
                    </Button>
                </Stack>
                <PageLoading loadingData={loadingData} />
                <Grow in={!loadingData}>
                   <Card>
                       <ListToolbar
                           filterQuery={filterQuery}
                           onFilterQuery={handleFilterByQuery}
                           properties={properties}
                           filterQueryProperty={filterQueryProperty}
                           setFilterQueryProperty={setFilterQueryProperty}
                           setFilterQuery={setFilterQuery}
                       />
                   </Card>
                </Grow>
            </Container>
        </Page>
    )
}