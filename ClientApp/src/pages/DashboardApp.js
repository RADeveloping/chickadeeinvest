// @mui
import {Container, Grid, Stack, Typography} from '@mui/material';
// components
import Page from '../components/Page';
import useFetch from '../components/FetchData';
import {Link} from "react-router-dom";
import PageLoading from "../components/PageLoading";
import * as React from "react";
import {filterProperties, filterTicket, filterUnit} from "../utils/filters";
import Widget from "../sections/@dashboard/app/Widget";
import UserWidget from "../sections/@dashboard/app/UserWidget";
// ----------------------------------------------------------------------

export default function DashboardApp() {

    const ticketUri = '/api/Tickets';
    const unitUri = '/api/Units';
    const propertyUri = '/api/Properties';

    const accountUri = '/api/Account';
    const currentUnitUri = '/api/Units/current';

    const [tickets, ticketsError, ticketsLoading] = useFetch(ticketUri, filterTicket);
    const [units, unitsError, unitsLoading] = useFetch(unitUri, filterUnit);
    const [properties, propertiesError, propertiesLoading] = useFetch(propertyUri, filterProperties);

    const [account, accountError, accountLoading] = useFetch(accountUri);
    const [currentUnit, currentUnitError, currentUnitLoading] = useFetch(currentUnitUri, (d) => {
        d = d[0];
        d.property = d.property.address;
        return d;
    });
    const userLoading = accountLoading && currentUnitLoading
    const loadingData = ticketsLoading && unitsLoading && propertiesLoading && userLoading

    const openTickets = tickets.filter((ticket) => ticket.status === 0);

    const dashboardItems = [
        {
            item:
                <Link to="/authentication/profile" style={{textDecoration: 'none'}}>
                    <UserWidget account={account} unit={currentUnit} loading={userLoading}/>
                </Link>,
            for: [
                "Tenant",
                "PropertyManager"
            ]
        },
        {
            item:
                <Widget title="Open Tickets" uri={'tickets'} total={openTickets.length} items={openTickets}
                        icon={'ant-design:folder-open-outlined'} loading={ticketsLoading}/>
            ,
            for: [
                "Tenant",
                "PropertyManager"
            ]
        },
        {
            item:
                <Widget title="Properties" uri={'properties'} total={properties.length} items={properties}
                        icon={'bxs:building-house'} loading={propertiesLoading}/>,
            for: [
                "PropertyManager"
            ]
        },
        {
            item:
                <Widget title="Units" uri={'units'} total={units.length} items={units} icon={'bxs:door-open'}
                        loading={unitsLoading}/>,
            for: [
                "PropertyManager"
            ]
        }
    ]

    const isMemberOf = (userRoles, roles) => {
        if (!userRoles) return false
        for (let i = 0; i < userRoles.length; i++) {
            for (let j = 0; j < roles.length; j++) {
                if (userRoles[i] === roles[j]) {
                    return true
                }
            }
        }
        return false
    }

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
                <Grid item xs={12} sm={6} md={isBigWidget(index) ? 7 : 5}>
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
