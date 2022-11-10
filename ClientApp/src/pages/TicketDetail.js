import {useNavigate, useParams, useSearchParams} from "react-router-dom";
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
    const title = "Ticket"
    const {id} = useParams();
    const [searchParams] = useSearchParams();
    const uid = searchParams.get('uid')
    const pid = searchParams.get('pid')
    const navigate = useNavigate();
    const isDesktop = useResponsive('up', 'lg');

    const [ticket, errorTicket, loadingTicket] = useFetch(`/api/properties/${pid}/units/${uid}/tickets/${id}`);
    const [unit, errorUnit, loadingUnit] = useFetch(uid ? `/api/properties/${pid}/units/${uid}` : null);
    const [property, errorProperty, loadingProperty] = useFetch(pid ? `/api/properties/${pid}` : null);

    const {createdOn, description, estimatedDate, problem, severity, status, tenant} = ticket;
    const {unitNo} = unit;
    const {address} = property;
    const loadingData = loadingTicket || loadingUnit || loadingProperty;
    
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
                        startIcon={<Iconify icon="akar-icons:check"/>}
                    >
                        {`Complete`}
                    </Button>
                </Stack>
                <PageLoading loadingData={loadingData}/>
                <Grow in={!loadingData}>
                    <Card>
                        {ticket.length !== 0 &&
                            <Grid container padding={3} spacing={3} direction={'column'}>
                                <Grid item>
                                    <Stack direction={'column'} gap={1}>
                                        <Typography variant={'h4'}>
                                            {problem}
                                        </Typography>
                                        <Stack direction={isDesktop ? 'row' : 'column'} justifyContent={'space-between'}
                                               gap={1}>
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
                                                    {new Date(createdOn).toLocaleDateString('en-CA', {dateStyle: 'medium'})}
                                                </Label>
                                                <Label sx={{fontWeight: 'normal'}}>
                                                    <div>
                                                        Estimated: <b>{new Date(estimatedDate).toLocaleDateString('en-CA', {dateStyle: 'medium'})}</b>
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
                                                {property.propertyManager && property.propertyManager.firstName} {property.propertyManager && property.propertyManager.lastName}
                                            </Typography>
                                        </Grid>
                                        <Grid item>
                                            <Typography sx={{fontSize: 14}} color="text.secondary" gutterBottom>
                                                Tenant
                                            </Typography>
                                            <Typography variant={'h6'} sx={{fontWeight: 'normal'}}>
                                                {tenant && tenant.firstName} {tenant && tenant.lastName}
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