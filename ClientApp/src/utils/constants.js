import {Grid, Stack, Typography} from "@mui/material";
import * as React from "react";
import Label from "../components/common/Label";

export const TICKETS_API = '/api/tickets';
export const UNITS_API = '/api/units';
export const PROPERTIES_API = '/api/properties';
export const ACCOUNTS_API = '/api/account';
export const getApiTicketUri = (pid, uid, id) =>
    `/api/properties/${pid}/units/${uid}/tickets/${id}`;
export const getApiTicketsUri = (pid, uid) =>
    `/api/properties/${pid}/units/${uid}/tickets`;
export const getApiUnitsUri = (pid) =>
    `/api/properties/${pid}/units`;
export const getTicketsUri = (ticket) =>
    `tickets/${ticket.ticketId}?pid=${ticket.propertyId}&uid=${ticket.unitId}`;
export const getPropertiesUri = (property) =>
    `tickets?property=${property.propertyId}`;
export const getUnitsUri = (unit) =>
    `tickets?property=${unit.propertyId}&unit=${unit.unitId}`;

export const SEVERITY = {
    0: {color: 'success', text: 'Low'},
    1: {color: 'warning', text: 'Medium'},
    2: {color: 'error', text: 'High'}
}

export const STATUS = {
    0: {color: 'info', text: 'Open'},
    1: {color: 'primary', text: 'Closed'},
}

export const PROPERTY_PROPS = [
    {id: 'address', label: 'Address'},
    {id: 'open_count', label: 'Open Ticket Count'},
    {id: 'unit_count', label: 'Unit Count'},
    {id: 'tenants_count', label: 'Tenant Count'},
    {id: 'name', label: 'Property Name'},
];

export const UNIT_PROPS = [
    {id: 'number', label: 'Unit Number'},
    {id: 'type', label: 'Unit Type'},
];

export const TICKET_PROPS = [
    {id: 'id', label: 'Ticket Id'},
    {id: 'createdOn', label: 'Created On'},
    {id: 'estimatedDate', label: 'Estimated Date'},
    {id: 'problem', label: 'Problem'},
    {id: 'severity', label: 'Severity'},
    {id: 'status', label: 'Status'},
];

export const isMemberOf = (userRoles, roles) => {
    if (!userRoles) return false
    for (let i = 0; i < userRoles.length; i++) {
        for (let j = 0; j < roles.length; j++) {
            if (userRoles[i] === roles[j]) {
                return true
            }
        }
    }
    return false
}

export const getTicketBox = (ticket) => {
    return (
        <>
            <Grid key={ticket.ticketId} container justifyContent={'space-between'} alignItems={'center'}>
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

            <Stack key={ticket.ticketId + 'stack'} direction={'row'} spacing={1}>
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
            <Grid key={unit.unitNo} container justifyContent={'space-between'} alignItems={'center'}>
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
            <Grid key={property.name} container justifyContent={'space-between'} alignItems={'center'}>
                <Grid item>
                    <b>{property.name}</b>
                </Grid>
            </Grid>
            <Stack key={property.name + 'stack'} direction={'row'} spacing={1}>
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
            dir: d.name,
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
                estimatedDate: d.estimatedDate ? new Date(d.estimatedDate) : null,
            }
        );
    })
    return simpleData;
}