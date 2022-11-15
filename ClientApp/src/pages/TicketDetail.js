import {useNavigate, useParams, useSearchParams} from "react-router-dom";
import {
    Avatar,
    Button,
    Card,
    Container,
    Divider,
    Grid,
    Grow,
    Stack,
    Tooltip,
    Typography,
    AvatarGroup
} from "@mui/material";
import * as React from "react";
import Page from "../components/common/Page";
import Iconify from "../components/common/Iconify";
import PageLoading from "../components/common/PageLoading";
import useFetch from "../utils/fetch";
import {SEVERITY, STATUS} from "../utils/constants";
import Label from "../components/common/Label";
import useResponsive from "../utils/responsive";

export default function TicketDetail() {
    const title = "Ticket"
    const {id} = useParams();
    const [searchParams] = useSearchParams();
    const uid = searchParams.get('uid')
    const pid = searchParams.get('pid')
    const navigate = useNavigate();
    const isDesktop = useResponsive('up', 'sm');

    const [ticket, errorTicket, loadingTicket] = useFetch(`/api/properties/${pid}/units/${uid}/tickets/${id}`);

    const {createdOn, description, estimatedDate, problem, severity, status, tenant, unit} = ticket;

    const loadingData = loadingTicket;

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
                            <Grid container spacing={3} direction={'column'}>
                                <Grid item>
                                    <Stack direction={'column'} padding={3} gap={1}>
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
                                                {estimatedDate &&
                                                    <Label sx={{fontWeight: 'normal'}}>
                                                        <div>
                                                            Estimated: <b>{new Date(estimatedDate).toLocaleDateString('en-CA', {dateStyle: 'medium'})}</b>
                                                        </div>
                                                    </Label>
                                                }
                                            </Stack>
                                        </Stack>
                                    </Stack>
                                </Grid>
                                <Divider/>
                                <Grid item>
                                    <Grid container padding={4} paddingTop={0} spacing={isDesktop ? 5 : 4}
                                          alignItems={'center'} justifyContent={''}>
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
                                            {ticket.unit.property &&
                                                <Typography variant={'h6'} sx={{fontWeight: 'normal'}}>
                                                    Unit #{ticket.unit.unitNo}, {ticket.unit.property.address}
                                                </Typography>
                                            }
                                        </Grid>
                                        <Grid item>
                                            <Typography sx={{fontSize: 14}} color="text.secondary" gutterBottom>
                                                Property Name
                                            </Typography>
                                            {ticket.unit.property &&
                                                <Typography variant={'h6'} sx={{fontWeight: 'normal'}}>
                                                    {ticket.unit.property.name}
                                                </Typography>
                                            }
                                        </Grid>
                                        <Grid item>
                                            <Typography sx={{fontSize: 14}} color="text.secondary" gutterBottom>
                                                Property Manager
                                            </Typography>
                                            {ticket.unit.propertyManager &&
                                                <Tooltip
                                                    title={`${ticket.unit.propertyManager.firstName} ${ticket.unit.propertyManager.lastName}`}
                                                    arrow>
                                                    <Avatar
                                                        alt={`${ticket.unit.propertyManager.firstName} ${ticket.unit.propertyManager.lastName}`}
                                                        src={ticket.unit.propertyManager.profilePicture ? `data:image/jpeg;base64,${ticket.unit.propertyManager.profilePicture}` : 'd'}/>
                                                </Tooltip>
                                            }
                                        </Grid>
                                        <Grid item>
                                            <Typography sx={{fontSize: 14}} color="text.secondary" gutterBottom>
                                                Tenants
                                            </Typography>
                                            {ticket.unit.tenants &&
                                                <AvatarGroup max={4}>
                                                    {ticket.unit.tenants.map((tenant) =>
                                                        <Tooltip key={tenant.firstName}
                                                                 title={`${tenant.firstName} ${tenant.lastName}`} arrow>
                                                            <Avatar key={tenant.lastName}
                                                                    alt={`${tenant.firstName} ${tenant.lastName}`}
                                                                    src={`data:image/jpeg;base64,${tenant.profilePicture}`}/>
                                                        </Tooltip>
                                                    )}
                                                </AvatarGroup>
                                            }
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