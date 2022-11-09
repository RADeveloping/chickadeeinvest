import {Card, CardContent, Grid, Grow, Stack, Typography} from "@mui/material";
import Label from "./Label";
import * as React from "react";

export default function Property({ data }) {
    const {address, propertyId, outstandingTickets, unitsCount, name, tenantsCount} = data;
    return (
        <Grow in={true}>
            <Grid xs={12} sm={12} md={6} l={4} xl={4} item>
                <Card sx={{height: 150}}>
                    <CardContent sx={{height: '100%'}}>
                        <Stack direction={'column'} justifyContent={'space-between'} alignItems={'flex-start'}
                               sx={{height: '100%'}}>
                            <Stack direction={'column'} width={'100%'}>
                                <Typography variant={'h4'} noWrap>
                                    {name}
                                </Typography>
                                <Typography variant={'h6'} color="text.secondary" noWrap>
                                    {address}
                                </Typography>
                            </Stack>
                            <Stack direction={'row'} alignItems={'center'} justifyContent={'left'}
                                   gap={1}>
                                <Label>
                                    {unitsCount} unit{unitsCount !== 1 && 's'}
                                </Label>
                                <Label color={'info'}>
                                    {outstandingTickets} open
                                    ticket{outstandingTickets !== 1 && 's'}
                                </Label>
                                <Label color={'success'}>
                                    {tenantsCount} tenant{tenantsCount !== 1 && 's'}
                                </Label>
                            </Stack>
                        </Stack>
                    </CardContent>
                </Card>
            </Grid>
        </Grow>
    )
}