import {Chip, Grid, Stack, Typography} from "@mui/material";
import Label from "../components/Label";
import * as React from "react";

export const SEVERITY = {
    0: {color: 'success', text: 'Low'},
    1: {color: 'warning', text: 'Medium'},
    2: {color: 'error', text: 'High'}
}

export const STATUS = {
    0: {color: 'info', text: 'Open'},
    1: {color: 'primary', text: 'Closed'},
}

export const propertyProperties = [
    {id: 'address', label: 'Address'},
    {id: 'open_count', label: 'Open Ticket Count'},
    {id: 'unit_count', label: 'Unit Count'},
    {id: 'tenants_count', label: 'Tenant Count'},
    {id: 'name', label: 'Property Name'},
];

export const unitProperties = [
    {id: 'number', label: 'Unit Number'},
    {id: 'type', label: 'Unit Type'},
];

export const ticketProperties = [
    {id: 'id', label: 'Ticket Id'},
    {id: 'createdOn', label: 'Created On'},
    {id: 'estimatedDate', label: 'Estimated Date'},
    {id: 'problem', label: 'Problem'},
    {id: 'severity', label: 'Severity'},
    {id: 'status', label: 'Status'},
];


export const getTicketBox = (ticket) => {
    return (
        <>
            <Grid container justifyContent={'space-between'} alignItems={'center'}>
                <Grid item>
                    <b>#{ticket.ticketId} {ticket.problem}</b>
                </Grid>
                <Grid item>
                    <Typography
                        sx={{display: 'inline'}}
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
                    <b>{unit.unitNo}</b>
                </Grid>
                <Grid item>
                    <Typography variant="body2"
                                color="text.secondary">
                        {unit.propertyName}
                    </Typography>
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
                    <b>{property.name}</b>
                </Grid>
            </Grid>
            <Stack direction={'row'} spacing={1}>
                <Label>
                    {property.unitsCount} unit{property.unitsCount !== 1 && 's'}
                </Label>
                <Label color={'info'}>
                    {property.outstandingTickets} open
                    ticket{property.outstandingTickets !== 1 && 's'}
                </Label>
                <Label color={'success'}>
                    {property.tenantsCount} tenant{property.tenantsCount !== 1 && 's'}
                </Label>
            </Stack>
        </>
    )
}

export const filterProperties = (data) => {
    let simpleData = []
    data.forEach((d) => {
        simpleData.push({
            id: d.propertyId,
            primary: getPropertiesBox(d),
            tertiary: d.address,
            dir: d.address,
            unitCount: d.units ? d.units.length : 0,
            ...d
        });
    })
    return simpleData;
}

export const filterUnit = (data) => {
    let simpleData = []
    data.forEach((d) => {
        simpleData.push({
            id: d.unitId,
            fid: d.propertyId,
            primary: getUnitBox(d),
            dir: d.unitNo,
            tenantCount: d.tenants ? d.tenants.length : 0,
            ...d
        });
    })
    return simpleData;
}

export const filterTicket = (data) => {
    let simpleData = []
    data.forEach((d) => {
        simpleData.push({
                id: d.ticketId,
                fid: d.unitId,
                primary: getTicketBox(d),
                tertiary: d.description,
                status: d.status,
                dir: `#${d.ticketId}`,
                ...d,
                createdOn: new Date(d.createdOn),
                estimatedDate: new Date(d.estimatedDate),
            }
        );
    })
    return simpleData;
}

export const getTicketsUri = (ticket) => {
    console.log(ticket)
    return `tickets/${ticket.ticketId}?pid=${ticket.propertyId}&uid=${ticket.unitId}`
}

export const getPropertiesUri = (property) => {
    console.log(property)
    return `tickets?property=${property.propertyId}`
}

export const getUnitsUri = (unit) => {
    console.log(unit)
    return `tickets?property=${unit.propertyId}&unit=${unit.unitId}`
}
