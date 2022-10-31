import {Link as RouterLink, useNavigate, useParams} from "react-router-dom";
import {Button, Card, Container, Grow, Stack, Typography} from "@mui/material";
import * as React from "react";
import Page from "../components/Page";
import Iconify from "../components/Iconify";
import PageLoading from "../components/PageLoading";
import useFetch from "../components/FetchData";

export default function TicketDetail() {
    const filterTickets = (data) => {
        data.forEach((d)=> {
            d.createdOn = new Date(d.createdOn)
            d.estimatedDate = new Date(d.estimatedDate)
        })
        return data;
    }
    const title = "Ticket"
    const { id } = useParams();
    const navigate = useNavigate();
    
    const [ticket, errorTicket, loadingTicket] = useFetch(`/api/Tickets/${id}`, filterTickets);
    const [unit, errorUnit, loadingUnit] = useFetch(ticket.unitId ? `/api/Units/${ticket.unitId}` : null);
    const [property, errorProperty, loadingProperty] = useFetch(unit.propertyId ? `/api/Properties/${unit.propertyId}` : null);
    const [propertyManager, errorPropertyManager, loadingPropertyManager] = useFetch(property.propertyManagerId ? `/api/Account/${property.propertyManagerId}` : null);
    
    const { createdOn, description, estimatedDate, problem, severity, status } = ticket;
    const { unitNo } = unit;
    const { address } = property;
    const loadingData = loadingTicket || loadingUnit || loadingProperty || loadingPropertyManager;

    return (
        <Page title={`${title} #${id}`}>
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Stack direction="column">
                        <Typography variant="h4" gutterBottom>
                            {title} #{id}
                        </Typography>
                        <Button
                            variant="contained"
                            startIcon={<Iconify icon="eva:arrow-back-outline" />}
                            onClick={()=>navigate(-1)}
                        >
                            Back
                        </Button>
                    </Stack>
                </Stack>
                <PageLoading loadingData={loadingData} />
                <Grow in={!loadingData}>
                    <Card sx={{display: loadingData ? 'none' : undefined}}>
                        {createdOn}
                    </Card>
                </Grow>
            </Container>
        </Page>
    )
}