// @mui
import { Container, Typography } from '@mui/material';
// components
import Page from '../components/Page';
import { useFetch } from '../components/FetchData';
import TicketList from '../components/TicketList';

// ----------------------------------------------------------------------

export default function DashboardApp() {
  
  const url = '/api/Unit/current';
  const { data } = useFetch(url);
  
  console.log(data);
  if (data && data.length) {
    const tickets = data[0].tickets;

    return (
      <Page title="Dashboard">
        <Container maxWidth="xl">
          <Typography variant="h4" sx={{ mb: 5 }}>
            Hi, Welcome back
          </Typography>
          <TicketList props={tickets} />
        </Container>
      </Page>
    );
  } else {
    return (
      <Page title="Dashboard">
        <Container maxWidth="xl">
          <Typography variant="h4" sx={{ mb: 5 }}>
            Hi, Welcome back
          </Typography>
          <p>Nothing here</p>
        </Container>
      </Page>
    );
  }
}
