import * as React from 'react';
import {useEffect, useState} from 'react';
import useFetch from "../components/FetchData";
import {Box, Container, Grow, Stack, Typography} from "@mui/material";
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
            #{ticket.ticketId} {ticket.problem}
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
    
    const filterProperties = (data) => {
        let simpleData = []
        data.forEach((d)=> {
            simpleData.push({
                id: d.propertyId,
                primary: d.address});
        })
        return simpleData;
    }
    const filterUnit = (data) => {
        let simpleData = []
        data.forEach((d)=> {
            simpleData.push({
                id: d.unitId,
                fid: d.propertyId,
                primary: `${d.unitNo}`});
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
            setPath(`${selectedProperty.primary}/Units/${selectedUnit.primary}`)
        } else if(selectedProperty) {
            setPath(`${selectedProperty.primary}`)
        }
    }, [selectedUnit, selectedProperty])
    
    const viewList = [
        <SimpleList items={properties} title={"Properties"} setSelect={setSelectedProperty} selected={selectedProperty}
         isDesktop={isDesktop} />,
        <SimpleList skinny items={ selectedProperty ?
            units.filter((u)=> u.fid === selectedProperty.id) : []}
                    title={"Units"} setNestedSelect={setSelectedProperty} path={path} setSelect={setSelectedUnit} selected={selectedUnit}
                    isDesktop={isDesktop} />,
        <SimpleList items={ selectedUnit ? tickets.filter((t)=> t.fid === selectedUnit.id) : []}
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
                    getActiveList()
                }
            </Container>
        </Page>
      
    )
}
