import {useEffect, useState} from "react";
import {Container, Stack, Typography} from "@mui/material";
import * as React from "react";
import Page from "../components/Page";


import ColumnOverview from "../components/ColumnOverview";

export default function SelectPropertyOverview() {
    return(
        <Page>
            <Container>
                <ColumnOverview />
            </Container>
        </Page>
    )
}