// @mui
import { useTheme } from '@mui/material/styles';
import {Container, Grid, Typography} from '@mui/material';
// components
import Page from '../components/Page';
import useFetch from '../components/FetchData';
import {AppWidgetSummary} from "../sections/@dashboard/app";
import {Link} from "react-router-dom";

// ----------------------------------------------------------------------

export default function DashboardApp() {
  const ticketUri = '/api/Tickets';
  const unitUri = '/api/Units';
  const propertyUri = '/api/Properties';
  
  const [tickets, ticketsError, ticketsLoading] = useFetch(ticketUri);
  const [units, unitsError, unitsLoading] = useFetch(unitUri);
  const [properties, propertiesError, propertiesLoading] = useFetch(propertyUri);
  
  const openTickets = tickets.filter((ticket) => ticket.status === 0);

  return (
    <Page title="Dashboard">
      <Container maxWidth="xl">
        <Typography variant="h4" sx={{ mb: 5 }}>
          Welcome back
        </Typography>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={6} md={3}>
            <Link to="/dashboard/tickets" style={{textDecoration: 'none'}}>
            <AppWidgetSummary title="Open Tickets" total={openTickets.length} color="error" icon={'ant-design:folder-open-outlined'} loading={ticketsLoading} />
            </Link>
          </Grid>
          <Grid item xs={12} sm={6} md={3}>
            <AppWidgetSummary title="Units" total={units.length} color="info" icon={'bxs:door-open'} loading={unitsLoading} />
          </Grid>
          <Grid item xs={12} sm={6} md={3}>
            <AppWidgetSummary title="Properties" color="success" total={properties.length} icon={'bxs:building-house'} loading={propertiesLoading} />
          </Grid>
        </Grid>
      </Container>
    </Page>
  );
}
