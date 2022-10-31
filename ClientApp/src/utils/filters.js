import {Chip, Grid, Stack, Typography} from "@mui/material";
import Label from "../components/Label";
import * as React from "react";
const SEVERITY = {
    0: {color: 'success', text: 'Low'},
    1: {color: 'warning', text: 'Medium'},
    2: {color: 'error', text: 'High'}
}

const STATUS = {
    0: {color: 'info', text: 'Open'},
    1: {color: 'primary', text: 'Closed'},
}

export const getTicketBox = (ticket) => {
    return (
        <>
            <Grid container justifyContent={'space-between'} alignItems={'center'}>
                <Grid item>
                    #{ticket.ticketId} {ticket.problem}
                </Grid>
                <Grid item>
                    <Typography
                        sx={{ display: 'inline' }}
                        component="span"
                        variant="body2"
                        color="text.secondary">
                        {new Date(ticket.createdOn).toLocaleDateString('en-CA', {dateStyle: 'medium'})}
                    </Typography>
                </Grid>
            </Grid>

            <Stack direction={'row'} spacing={1}>
                <Label
                    variant="ghost"
                    color={STATUS[ticket.status].color}
                >
                    {STATUS[ticket.status].text}
                </Label>
                <Label
                    variant="ghost"
                    color={SEVERITY[ticket.severity].color}
                >
                    {SEVERITY[ticket.severity].text}
                </Label>
            </Stack>
        </>
    )
}

export const getUnitBox = (unit) => {
    return (
        <>
            <Grid container justifyContent={'space-between'} alignItems={'center'}>
                <Grid item>
                    {unit.unitNo}
                </Grid>
                <Grid item>
                    <Chip label={unit.tickets.length} />
                </Grid>
            </Grid>
        </>
    )
}

export const getPropertiesBox = (property) => {
    return (
        <>
            <Grid container justifyContent={'space-between'} alignItems={'center'}>
                <Grid item>
                    {property.address}
                </Grid>
                <Grid item>
                    <Chip label={property.units.length} />
                </Grid>
            </Grid>
        </>
    )
}

export const filterProperties = (data) => {
    let simpleData = []
    data.forEach((d)=> {
        simpleData.push({
            id: d.propertyId,
            primary: getPropertiesBox(d),
            dir: d.address});
    })
    return simpleData;
}

export const filterUnit = (data) => {
    let simpleData = []
    data.forEach((d)=> {
        simpleData.push({
            id: d.unitId,
            fid: d.propertyId,
            primary: getUnitBox(d),
            dir: d.unitNo});
    })
    return simpleData;
}

export const filterTicket = (data) => {
    let simpleData = []
    data.forEach((d)=> {
        simpleData.push({
                id: d.ticketId,
                fid: d.unitId,
                primary: getTicketBox(d),
                tertiary: d.description,
                status: d.status,
                dir: `#${d.ticketId}`,
            }
        );
    })
    return simpleData;
}