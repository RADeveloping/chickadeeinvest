import {Card, CardContent, Grid, Grow, Stack, Typography} from "@mui/material";
import * as React from "react";
import {formatDate, getTicketsUri, SEVERITY, STATUS} from "../../utils/constants";
import Label from "../common/Label";

/**
 * Ticket component to display search results for tickets.
 * @param data Ticket data.
 * @param navigate Navigate method from useNavigation.
 * @returns {JSX.Element}
 * @constructor
 */
export default function Ticket({data, navigate}) {
    const {ticketId, problem, description, createdOn, estimatedDate, status, severity} = data;
    return (
        <Grow key={'ticket' + ticketId} in={true}>
            <Grid xs={12} sm={12} md={6} l={4} xl={4} item>
                <Card sx={{height: 150}} onClick={() => navigate('/dashboard/' + getTicketsUri(data))}>
                    <CardContent sx={{height: '100%'}}>
                        <Stack direction={'column'} justifyContent={'space-between'} alignItems={'flex-start'}
                               sx={{height: '100%'}}>
                            <Stack direction={'column'} width={'100%'}>
                                <Typography variant={'h4'} noWrap>
                                    #{ticketId} {problem}
                                </Typography>
                                <Typography variant={'h6'} color="text.secondary" noWrap>
                                    {description}
                                </Typography>
                            </Stack>
                            <Stack width={'100%'} direction={'row'} justifyContent={'space-between'}>
                                <Stack direction={'row'} alignItems={'center'} justifyContent={'left'}
                                       gap={1}>
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
                                        {formatDate(createdOn)}
                                    </Label>
                                    {estimatedDate &&
                                        <Label sx={{fontWeight: 'normal'}}>
                                            <div>
                                                Estimated: <b>{formatDate(estimatedDate)}</b>
                                            </div>
                                        </Label>
                                    }
                                </Stack>
                            </Stack>
                        </Stack>
                    </CardContent>
                </Card>
            </Grid>
        </Grow>
    )
}