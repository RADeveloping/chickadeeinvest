﻿import {Card, CardContent, Grid, Grow, Stack, Typography} from "@mui/material";
import Label from "./Label";
import * as React from "react";
import {SEVERITY, STATUS} from "../utils/filters";

export default function Ticket({ data }) {
    const {ticketId, problem, description, createdOn, estimatedDate, status, severity} = data;
    return (
        <Grow in={true}>
            <Grid xs={12} sm={12} md={6} l={4} xl={4} item>
                <Card sx={{height: 150}}>
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
                    </CardContent>
                </Card>
            </Grid>
        </Grow>
    )
}