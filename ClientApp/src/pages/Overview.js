import * as React from 'react';
import {useEffect, useState} from 'react';
import useFetch from "../components/FetchData";
import {Box, Chip, Container, Grid, Grow, Stack, Typography} from "@mui/material";
import Page from "../components/Page";
import SimpleList from "../components/SimpleList";
import PageLoading from "../components/PageLoading";
import useResponsive from "../hooks/useResponsive";
import Label from "../components/Label";

const SEVERITY = {
    0: {color: 'success', text: 'Low'},
    1: {color: 'warning', text: 'Medium'},
    2: {color: 'error', text: 'High'}
}

const STATUS = {
    0: {color: 'info', text: 'Open'},
    1: {color: 'primary', text: 'Closed'},
}

export default function Overview() {
    const getTicketBox = (ticket) => {
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
    
    const getUnitBox = (unit) => {
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

    const getPropertiesBox = (property) => {
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
    
    const filterProperties = (data) => {
        let simpleData = []
        data.forEach((d)=> {
            simpleData.push({
                id: d.propertyId,
                primary: getPropertiesBox(d),
                dir: d.address});
        })
        return simpleData;
    }
    const filterUnit = (data) => {
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
    const filterTicket = (data) => {
        let simpleData = []
        data.forEach((d)=> {
            simpleData.push({
                    id: d.ticketId,
                    fid: d.unitId,
                    primary: getTicketBox(d),
                    tertiary: d.description,
                     dir: `#${d.ticketId}`,
                }
            );
        })
        return simpleData;
    }
    const [properties, errorProperties, loadingProperties] = useFetch('/api/Property', filterProperties);
    const [units, errorUnits, loadingUnits] = useFetch('/api/Unit', filterUnit);
    const [tickets, errorTickets, loadingTickets] = useFetch('/api/Ticket', filterTicket);
    
    const [selectedProperty, setSelectedProperty] = useState(null);
    const [selectedUnit, setSelectedUnit] = useState(null);
    const [selectedTicket, setSelectedTicket] = useState(null);
    const loadingData = loadingProperties && loadingUnits && loadingTickets;
    
    const [path, setPath] = useState('');

    const isDesktop = useResponsive('up', 'lg');

    const title = "Overview"
    
    useEffect(()=> {
        setSelectedUnit(null);
    }, [selectedProperty])

    useEffect(()=> {
        setSelectedTicket(null);
    }, [selectedUnit])
    
    useEffect(() => {
        if (selectedUnit) {
            setPath(`${selectedProperty.dir}/Units/${selectedUnit.dir}`)
        } else if(selectedProperty) {
            setPath(`${selectedProperty.dir}`)
        }
    }, [selectedUnit, selectedProperty])
    
    const viewList = [
        <SimpleList leftRound items={properties} title={"Properties"} setSelect={setSelectedProperty} selected={selectedProperty}
         isDesktop={isDesktop} />,
        <SimpleList noRound skinny items={ selectedProperty ?
            units.filter((u)=> u.fid === selectedProperty.id) : []}
                    title={"Units"} setNestedSelect={setSelectedProperty} path={path} setSelect={setSelectedUnit} selected={selectedUnit}
                    isDesktop={isDesktop} />,
        <SimpleList rightRound items={ selectedUnit ? tickets.filter((t)=> t.fid === selectedUnit.id) : []}
                    title={"Tickets"} setNestedSelect={setSelectedUnit} path={path} setSelect={setSelectedTicket} selected={selectedTicket}
                    isDesktop={isDesktop} />
    ]
    
    function getActiveList() {
        if (selectedProperty && selectedUnit) {
            return viewList[2]
        } else if (selectedProperty) {
            return viewList[1]
        } else {
            return viewList[0]
        }
    }
    
    return(
        
        <Page title={title}>
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        {title}
                    </Typography>
                </Stack>
                <PageLoading loadingData={loadingData} />
                {isDesktop &&
                <Grow in={!loadingData}>
                    <Stack direction="row">
                        {viewList}
                    </Stack>
                </Grow>
                }
                {!isDesktop &&
                    <Grow in={!loadingData}>
                        <Box>
                        {getActiveList()}
                        </Box>
                    </Grow>
                }
            </Container>
        </Page>
      
    )
}
