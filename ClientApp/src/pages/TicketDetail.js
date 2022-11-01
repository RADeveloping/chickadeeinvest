import {useNavigate, useParams} from "react-router-dom";
import {Button, Card, Container, Grid, Grow, Stack, Typography} from "@mui/material";
import * as React from "react";
import Page from "../components/Page";
import Iconify from "../components/Iconify";
import PageLoading from "../components/PageLoading";
import useFetch from "../components/FetchData";
import {SEVERITY, STATUS} from "../utils/filters";
import Label from "../components/Label";
import useResponsive from "../hooks/useResponsive";

export default function TicketDetail() {
    const filterTickets = (data) => {
        data.createdOn = new Date(data.createdOn)
        data.estimatedDate = new Date(data.estimatedDate)
        return data;
    }
    const title = "Ticket"
    const {id} = useParams();
    const navigate = useNavigate();
    const isDesktop = useResponsive('up', 'lg');

    const [ticket, errorTicket, loadingTicket] = useFetch(`/api/Tickets/${id}`, filterTickets);
    const [unit, errorUnit, loadingUnit] = useFetch(ticket.unitId ? `/api/Units/${ticket.unitId}` : null);
    const [property, errorProperty, loadingProperty] = useFetch(unit.propertyId ? `/api/Properties/${unit.propertyId}` : null);
    const [propertyManager, errorPropertyManager, loadingPropertyManager] = useFetch(property.propertyManagerId ? `/api/Account/${property.propertyManagerId}` : null);

    const {createdOn, description, estimatedDate, problem, severity, status, tenant} = ticket;
    const {unitNo} = unit;
    const {address} = property;
    const loadingData = loadingTicket || loadingUnit || loadingProperty || loadingPropertyManager;

    console.log(ticket, unit, property)
    return (
        <Page title={`${title} #${id}`}>
            <Container>
                <Stack direction="row" alignItems={'flex-end'} justifyContent="space-between" mb={5}>
                    <Stack direction="column">
                        <Typography variant="h4" gutterBottom>
                            {title} #{id}
                        </Typography>
                        <Button
                            variant="contained"
                            startIcon={<Iconify icon="eva:arrow-back-outline"/>}
                            onClick={() => navigate(-1)}
                        >
                            Back
                        </Button>
                    </Stack>
                    <Button
                        variant="contained"
                        to="#"
                        startIcon={<Iconify icon="akar-icons:check" />}
                    >
                        {`Complete`}
                    </Button>
                </Stack>
                <PageLoading loadingData={loadingData}/>
                <Grow in={!loadingData}>
                    <Card sx={{display: loadingData ? 'none' : undefined}}>
                        {!loadingData &&
                            <Grid container padding={3} spacing={3} direction={'column'}>
                                <Grid item>
                                    <Stack direction={'column'} gap={1}>
                                    <Typography variant={'h4'}>
                                        {problem}
                                    </Typography>
                                    <Stack direction={isDesktop ? 'row' : 'column'} justifyContent={'space-between'} gap={1}>
                                        <Stack direction={'row'} alignItems={'center'} gap={1}>
                                            <Label
                                                variant="ghost"
                                                color={STATUS[status].color}
                                            >
                                                {STATUS[status].text}
                                            </Label>
                                            <Label
                                                variant="ghost"
                                                color={SEVERITY[severity].color}
                                            >
                                                {SEVERITY[severity].text}
                                            </Label>
                                        </Stack>
                                        <Stack direction={'row'} alignItems={'center'} gap={1}>
                                            <Label>
                                                {createdOn.toLocaleDateString('en-CA', {dateStyle: 'medium'})}
                                            </Label>
                                            <Label sx={{fontWeight: 'normal'}}>
                                                <div>
                                                    Estimated: <b>{estimatedDate.toLocaleDateString('en-CA', {dateStyle: 'medium'})}</b>
                                                </div>
                                            </Label>
                                        </Stack>
                                    </Stack>
                                    </Stack>
                               
                                </Grid>

                                <Grid item>

                                    <Grid container spacing={4} alignItems={'center'} justifyContent={'space-between'}>

                                        <Grid item>
                                            <Typography sx={{fontSize: 14}} color="text.secondary" gutterBottom>
                                                Description
                                            </Typography>
                                            <Typography variant={'h6'} sx={{fontWeight: 'normal'}}>
                                                {description}
                                            </Typography>

                                        </Grid>
                                        <Grid item>
                                            <Typography sx={{fontSize: 14}} color="text.secondary" gutterBottom>
                                                Address
                                            </Typography>
                                            <Typography variant={'h6'} sx={{fontWeight: 'normal'}}>
                                                Unit #{unitNo}, {address}
                                            </Typography>
                                        </Grid>
                                        <Grid item>
                                            <Typography sx={{fontSize: 14}} color="text.secondary" gutterBottom>
                                                Property Manager
                                            </Typography>
                                            <Typography variant={'h6'} sx={{fontWeight: 'normal'}}>
                                                {property.propertyManager.firstName} {property.propertyManager.lastName}
                                            </Typography>
                                        </Grid>
                                        <Grid item>
                                            <Typography sx={{fontSize: 14}} color="text.secondary" gutterBottom>
                                                Tenant
                                            </Typography>
                                            <Typography variant={'h6'} sx={{fontWeight: 'normal'}}>
                                                {tenant.firstName} {tenant.lastName}
                                            </Typography>
                                        </Grid>
                                    </Grid>
                                </Grid>
                            </Grid>
                        }
                    </Card>
                </Grow>
            </Container>
        </Page>
    )
}