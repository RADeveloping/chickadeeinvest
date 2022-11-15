import {useEffect, useState} from "react";
import {Button, Container, Stack, Typography} from "@mui/material";
import * as React from "react";
import Page from "../components/common/Page";
import {ToggleButton, ToggleButtonGroup} from "@mui/material";
import Iconify from "../components/common/Iconify";

import {useSearchParams} from "react-router-dom";
import ColumnOverview from "../components/tickets/ColumnOverview";
import TableOverview from "../components/tickets/TableOverview";

export default function Tickets() {
    const [searchParams, setSearchParams] = useSearchParams();
    const title = "Tickets"
    const [viewMode, setViewMode] = useState('column');

    const handleViewModeChange = (event, newViewMode) => {
        if (newViewMode) {
            setViewMode(newViewMode)
            searchParams.set('viewMode', newViewMode)
            setSearchParams(searchParams)
        }
    };

    useEffect(() => {
        let view = searchParams.get('viewMode')
        if (view) setViewMode(view)
    }, [])

    return (
        <Page title={title}>
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        {title}
                    </Typography>
                    <ToggleButtonGroup
                        color="primary"
                        value={viewMode}
                        exclusive
                        onChange={handleViewModeChange}
                    >
                        <ToggleButton value="column"><Iconify icon="cil:view-column"/></ToggleButton>
                        <ToggleButton value="table"><Iconify icon="carbon:table-split"/></ToggleButton>
                    </ToggleButtonGroup>
                </Stack>
                <div style={{display: viewMode !== 'column' ? 'none' : undefined}}>
                    <ColumnOverview/>
                </div>
                <div style={{display: viewMode === 'column' ? 'none' : undefined}}>
                    <TableOverview/>
                </div>
            </Container>
        </Page>
    )
}