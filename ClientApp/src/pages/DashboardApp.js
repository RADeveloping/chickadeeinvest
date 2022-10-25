// @mui
import { Container, Typography } from '@mui/material';
// components
import Page from '../components/Page';
import useFetch from '../components/FetchData';
import TicketList from '../components/TicketList';

// ----------------------------------------------------------------------

export default function DashboardApp() {
  const url = '/api/Ticket';
  const { data, error } = useFetch(url);

  const openTickets = data.filter((ticket) => ticket.status === 0);

  console.log(data);
  if (!data) {
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
  return (
    <Page title="Dashboard">
      <Container maxWidth="xl">
        <Typography variant="h4" sx={{ mb: 5 }}>
          Hi, Welcome back
        </Typography>
        <TicketList props={openTickets} />
      </Container>
    </Page>
  );
}
