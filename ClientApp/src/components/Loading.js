import {Box, CircularProgress} from "@mui/material";
import * as React from "react";

export default function Loading({loading}) {
    return (
        loading ?
                <Box   display="flex"
                       justifyContent="center"
                       alignItems="center"
                       height={'100%'}
                       >
                    <CircularProgress />
                </Box> : null 
    )
}