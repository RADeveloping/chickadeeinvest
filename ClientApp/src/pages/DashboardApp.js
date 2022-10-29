// @mui
import { useTheme } from '@mui/material/styles';
import {Container, Grid, Typography} from '@mui/material';
// components
import Page from '../components/Page';
import useFetch from '../components/FetchData';
import {Link} from "react-router-dom";
import {UserWidget, Widget} from "../sections/@dashboard/app";

// ----------------------------------------------------------------------

export default function DashboardApp() {
  const ticketUri = '/api/Tickets';
  const unitUri = '/api/Units';
  const propertyUri = '/api/Properties';
  
  const accountUri = '/api/Account';
  const currentUnitUri = '/api/Units/current';
  
  const [tickets, ticketsError, ticketsLoading] = useFetch(ticketUri);
  const [units, unitsError, unitsLoading] = useFetch(unitUri);
  const [properties, propertiesError, propertiesLoading] = useFetch(propertyUri);

  const [account, accountError, accountLoading] = useFetch(accountUri);
  const [currentUnit, currentUnitError, currentUnitLoading] = useFetch(currentUnitUri, (d) => {
    d = d[0];
    d.property = d.property.address;
    return d;
  });
  const userLoading = accountLoading && currentUnitLoading
  
  const openTickets = tickets.filter((ticket) => ticket.status === 0);
console.log(currentUnit)
  return (
    <Page title="Dashboard">
      <Container maxWidth="l">
        <Grid height={'100%'} container spacing={3}>
          <Grid item xs={12} sm={6} md={5}>
            <Link to="/dashboard/tickets" style={{textDecoration: 'none'}}>
            <UserWidget account={account} unit={currentUnit} loading={userLoading} />
            </Link>
          </Grid>
          <Grid item xs={12} sm={6} md={7}>
            <Link to="" style={{textDecoration: 'none'}}>
              <Widget title="Open Tickets" total={openTickets.length} color="error" icon={'ant-design:folder-open-outlined'} loading={ticketsLoading} />
            </Link>
          </Grid>
          <Grid item xs={12} sm={6} md={7}>
            <Widget title="Units" total={units.length} color="info" icon={'bxs:door-open'} loading={unitsLoading} />
          </Grid>
          <Grid item xs={12} sm={6} md={5}>
            <Widget title="Properties" color="success" total={properties.length} icon={'bxs:building-house'} loading={propertiesLoading} />
          </Grid>
        </Grid>
      </Container>
    </Page>
  );
}
