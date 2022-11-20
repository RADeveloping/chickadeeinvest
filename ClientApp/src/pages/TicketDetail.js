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
import useFetch, {usePost} from "../utils/fetch";
import {ACCOUNTS_API, getApiTicketUri, isMemberOf, SEVERITY, STATUS} from "../utils/constants";
import Label from "../components/common/Label";
import useResponsive from "../utils/responsive";
import {useState} from "react";
import {LoadingButton} from "@mui/lab";

export default function TicketDetail() {
    const title = "Ticket"
    const {id} = useParams();
    const [searchParams] = useSearchParams();
    const uid = searchParams.get('uid')
    const pid = searchParams.get('pid')
    const navigate = useNavigate();
    const isDesktop = useResponsive('up', 'sm');
    const [patchTicket, setPatchTicket] = useState(null);

    const onFetch = () => {
        setLoadingCompleteButton(false);
    }
    const [ticket, errorTicket, loadingTicket, reloadTicket] = useFetch(getApiTicketUri(pid, uid, id), undefined,
        true, onFetch);

    const onPost = () => {
        reloadTicket()
    }
    const [respPatch, errorPatch, loadingPatch] = usePost(getApiTicketUri(pid, uid, id),
        undefined, patchTicket, onPost);

    const [account] = useFetch(ACCOUNTS_API);
    const showComplete = account ? isMemberOf(account.roles, ["SuperAdmin", "PropertyManager"]) : null;

    const [loadingCompleteButton, setLoadingCompleteButton] = useState(false);
    const {createdOn, description, estimatedDate, problem, severity, status, closedDate} = ticket;
    const firstLoad = ticket.length === 0;
    const loadingData = loadingTicket && firstLoad;

    const setCompletedButton = () => {
        setLoadingCompleteButton(true);
        setPatchTicket([
            {
                "op": "replace",
                "path": "/status",
                "value": 1
            }
        ])
    }
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
                    <Grow in={showComplete === true && !loadingData}>
                        <LoadingButton
                            loading={loadingCompleteButton}
                            onClick={setCompletedButton}
                            disabled={status === 1 ? true : false}
                            variant="contained"
                            to="#"
                            startIcon={<Iconify icon="akar-icons:check"/>}
                        >
                            {status === 0 ? "Complete" : "Completed"}
                        </LoadingButton>
                    </Grow>
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
                                                <Label sx={{fontWeight: 'normal'}}>
                                                    <div>
                                                        Opened: <b>{new Date(createdOn).toLocaleDateString('en-CA', {dateStyle: 'medium'})}</b>
                                                    </div>
                                                </Label>
                                                {estimatedDate && !closedDate &&
                                                    <Label sx={{fontWeight: 'normal'}}>
                                                        <div>
                                                            Estimated: <b>{new Date(estimatedDate).toLocaleDateString('en-CA', {dateStyle: 'medium'})}</b>
                                                        </div>
                                                    </Label>
                                                }
                                                {closedDate &&
                                                    <Label color={'primary'} sx={{fontWeight: 'normal'}}>
                                                        <div>
                                                            Closed: <b>{new Date(closedDate).toLocaleDateString('en-CA', {dateStyle: 'medium'})}</b>
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
                                          alignItems={'center'}>
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
                                                <Tooltip enterTouchDelay={0}
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
                                                        <Tooltip enterTouchDelay={0} key={tenant.firstName}
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