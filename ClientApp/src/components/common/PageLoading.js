import {Box, CircularProgress, Grow} from "@mui/material";
import * as React from "react";

/**
 * Component for page loading.
 * @param loadingData {boolean} Loading state.
 * @returns {JSX.Element|null}
 * @constructor
 */
export default function PageLoading({loadingData}) {
    return (
        loadingData ?
            <Box display="flex"
                 justifyContent="center"
                 alignItems="center"
                 height="50vh">
                <Grow timeout={{enter: 3000}} in={loadingData}><CircularProgress/></Grow>
            </Box> : null
    )
}