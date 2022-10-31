import {Link as RouterLink, useNavigate, useParams} from "react-router-dom";
import {Button, Card, CardContent, Container, Grid, Grow, Stack, Typography} from "@mui/material";
import * as React from "react";
import Page from "../components/Page";
import Iconify from "../components/Iconify";
import PageLoading from "../components/PageLoading";
import useFetch from "../components/FetchData";
import {SEVERITY, STATUS} from "../utils/filters";
import Label from "../components/Label";

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
                        {!loadingData &&
                            <Grid container padding={4} spacing={5} direction={'column'}>
                        
                        <Grid item>
                            <Stack direction={'row'} alignItems={'center'} gap={1}>
                                <Typography variant={'h4'}>
                                    {problem}
                                </Typography>
                                <Label
                                    variant="ghost"
                                    color={SEVERITY[severity].color}
                                >
                                    {SEVERITY[severity].text}
                                </Label>
                                <Label
                                    variant="ghost"
                                    color={STATUS[status].color}
                                >
                                    {STATUS[status].text}
                                </Label>
                            </Stack>
                        </Grid>
                      
                        <Grid item>
                            <Typography variant={'h6'}>
                                {description}
                            </Typography>
                        </Grid>
                        
                    </Grid>
                        }
                    </Card>
                </Grow>
            </Container>
        </Page>
    )
}