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
import {getPropertiesUri, getUnitBox, propertyProperties} from "../utils/filters";
import Label from "../components/Label";
import {ToggleButton, ToggleButtonGroup} from "@mui/lab";
import Property from "../components/Property";
import useFilter from "../components/FilterOrder";

export default function Properties() {
    const uri = '/api/Properties'
    const title = "Properties"
    const dataName = 'Property';
    const [urlSearchParams,
        orderBy, setOrderBy, handleOrderByChange,
        order, setOrder, handleOrderChange,
        filterQuery, handleFilterByQuery, setFilterQuery] = useFilter(propertyProperties);
    const [data, errorData, loadingData] = useFetch(uri + '?' + urlSearchParams.toString());

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
                <PageLoading loadingData={loadingData}/>
                <Grow in={!loadingData}>
                    <Stack direction={isDesktop ? 'row' : 'column'} gap={1} alignItems={'center'}
                           justifyContent={'space-between'}>
                        <Card sx={{
                            boxShadow: 'rgba(149, 157, 165, 0.2) 0px 8px 24px',
                            display: loadingData ? 'none' : undefined,
                            width: 'fit-content',
                            backgroundColor: (theme) => theme.palette['background'].default,
                        }}>

                            <ListToolbar
                                filterQuery={filterQuery}
                                onFilterQuery={handleFilterByQuery}
                                properties={propertyProperties}
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
                            <Stack direction={'row'} alignItems={'stretch'} gap={1}>
                                <FormControl sx={{minWidth: 80}}>
                                    <InputLabel id="sort-property">Order by</InputLabel>
                                    <Select
                                        labelId="sort-property"
                                        value={orderBy}
                                        label="Order by"
                                        onChange={handleOrderByChange}
                                    >
                                        {propertyProperties.map((p) =>
                                            <MenuItem key={p.id} value={p.id}>{p.label}</MenuItem>
                                        )}
                                    </Select>
                                </FormControl>
                                <ToggleButtonGroup
                                    color="primary"
                                    value={order}
                                    exclusive
                                    onChange={handleOrderChange}
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
                <Grow in={!loadingData && data.length > 0}>
                    <Grid container spacing={1} justifyContent={isDesktop ? undefined : 'center'}>
                        {data.map((data) =>
                                // <Link to={'/dashboard/' + getPropertiesUri(data)} style={{textDecoration: 'none'}}>
                                <Property data={data}/>
                            // </Link>
                        )}
                    </Grid>
                </Grow>
                {!loadingData && data.length === 0 &&
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