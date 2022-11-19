import {Box} from "@mui/material";
import * as React from "react";

export default function NoItems({items, title, height = '80%'}) {
    return (<>
        {
            items.length === 0 &&
            <Box sx={{
                height: height,
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                color: 'gainsboro'
            }}>{`No ${title}`}</Box>
        }
    </>);

}