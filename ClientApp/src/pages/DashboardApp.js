// @mui
import { useTheme } from '@mui/material/styles';
import {Container, Grid, Stack, Typography} from '@mui/material';
// components
import Page from '../components/Page';
import useFetch from '../components/FetchData';
import {Link} from "react-router-dom";
import {UserWidget, Widget} from "../sections/@dashboard/app";
import PageLoading from "../components/PageLoading";
import * as React from "react";
import {filterProperties, filterTicket, filterUnit} from "../utils/filters";

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
console.log(tickets)
  return (
    <Page title="Dashboard">
      <Container>
        <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
          <Typography variant="h4" gutterBottom>
            Dashboard
          </Typography>
        </Stack>
        <PageLoading loadingData={loadingData} />
        <Grid height={'100%'} container spacing={3}>
          <Grid item xs={12} sm={6} md={5}>
            <Link to="/authentication/profile" style={{textDecoration: 'none'}}>
            <UserWidget account={account} unit={currentUnit} loading={userLoading} />
            </Link>
          </Grid>
          <Grid item xs={12} sm={6} md={7}>
            <Link to="" style={{textDecoration: 'none'}}>
              <Widget title="Open Tickets" total={openTickets.length} items={openTickets} icon={'ant-design:folder-open-outlined'} loading={ticketsLoading} />
            </Link>
          </Grid>
          <Grid item xs={12} sm={6} md={7}>
            <Widget title="Properties" total={properties.length} items={properties} icon={'bxs:building-house'} loading={propertiesLoading} />
          </Grid>
          <Grid item xs={12} sm={6} md={5}>
            <Widget title="Units" total={units.length} items={units} icon={'bxs:door-open'} loading={unitsLoading} />
          </Grid>
        </Grid>
      </Container>
    </Page>
  );
}
