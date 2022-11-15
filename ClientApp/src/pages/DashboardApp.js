// @mui
import {Container, Grid, Stack, Typography} from '@mui/material';
// components
import Page from '../components/common/Page';
import useFetch from '../utils/fetch';
import {Link} from "react-router-dom";
import PageLoading from "../components/common/PageLoading";
import * as React from "react";
import {
    accountUri,
    filterProperties,
    filterTicket,
    filterUnit,
    getPropertiesUri,
    getTicketsUri,
    getUnitsUri, isMemberOf, propertyUri, ticketUri, unitUri
} from "../utils/constants";
import AddTicket from "../components/overlay/AddTicket";
import Widget from "../components/dashboard/Widget";
import UserWidget from "../components/dashboard/UserWidget";
// ----------------------------------------------------------------------

export default function DashboardApp() {

    const [tickets, ticketsError, ticketsLoading] = useFetch(ticketUri, filterTicket);
    const [units, unitsError, unitsLoading] = useFetch(unitUri, filterUnit);
    const [properties, propertiesError, propertiesLoading] = useFetch(propertyUri + '?sort=desc&param=open_count', filterProperties);

    const [account, accountError, accountLoading] = useFetch(accountUri);

    const userLoading = accountLoading
    const loadingData = ticketsLoading && unitsLoading && propertiesLoading && userLoading

    const openTickets = tickets.filter((ticket) => ticket.status === 0);

    const dashboardItems = [
        {
            item:
                <Link to="/authentication/profile" style={{textDecoration: 'none'}}>
                    <UserWidget account={account} loading={userLoading}/>
                </Link>,
            for: [
                "Tenant",
                "PropertyManager"
            ]
        },
        {
            item:
                <Widget title="Open Tickets" uri={getTicketsUri} total={openTickets.length} items={openTickets}
                        icon={'ant-design:folder-open-outlined'}
                        addComponent={
                            account.roles ?
                                !account.roles.includes('PropertyManager') ?
                                    (open, handleClose) => <AddTicket open={open}
                                                                      handleClose={handleClose}/> : undefined
                                : undefined
                        }
                        loading={ticketsLoading}
                />
            ,
            for: [
                "Tenant",
                "PropertyManager"
            ]
        },
        {
            item:
                <Widget title="Properties" uri={getPropertiesUri} total={properties.length} items={properties}
                        icon={'bxs:building-house'} loading={propertiesLoading}/>,
            for: [
                "PropertyManager"
            ]
        },
        {
            item:
                <Widget title="Units" uri={getUnitsUri} total={units.length} items={units} icon={'bxs:door-open'}
                        loading={unitsLoading}/>,
            for: [
                "PropertyManager"
            ]
        }
    ]

    const isBigWidget = (index) => {
        let remainder = index % 2
        let row = Math.ceil((index + 1) / 2)
        let remainderRow = row % 2
        return (remainderRow === 0 ? remainder === 0 : remainder !== 0)
    }

    const getDashboardLayout = () => {
        let items = dashboardItems.filter(item => isMemberOf(account.roles, item.for))
        return items.map((item, index) =>
            (
                <Grid key={`${index}`} item xs={12} sm={6} md={isBigWidget(index) ? 7 : 5}>
                    {item.item}
                </Grid>
            ))
    }

    return (
        <Page title="Dashboard">
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        Dashboard
                    </Typography>
                </Stack>
                <PageLoading loadingData={loadingData}/>
                <Grid height={'100%'} container spacing={3}>
                    {getDashboardLayout()}
                </Grid>
            </Container>
        </Page>
    );
}
