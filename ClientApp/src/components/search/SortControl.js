import {Card, FormControl, InputLabel, MenuItem, Select, Stack, Typography} from "@mui/material";
import {ToggleButton, ToggleButtonGroup} from "@mui/material";
import * as React from "react";
import Iconify from "../common/Iconify";

export default function SortControl({
                                        title,
                                        orderBy,
                                        handleOrderByChange,
                                        properties,
                                        order,
                                        handleOrderChange,
                                    }) {
    return (
        <Card sx={{
            boxShadow: 'rgba(149, 157, 165, 0.2) 0px 8px 24px',
            width: '100%',
            backgroundColor: (theme) => theme.palette['background'].default,
            padding: 2.5
        }}>
            <Stack direction={'row'} alignItems={'center'} justifyContent={'space-between'} gap={1}>
                <Typography variant={'h4'}>
                    {title}
                </Typography>
                <Stack direction={'row'} alignItems={'stretch'} gap={1}>
                    <FormControl sx={{minWidth: 80}}>
                        <InputLabel id="sort-property">Order by</InputLabel>
                        <Select
                            labelId="sort-property"
                            value={orderBy}
                            label="Order by"
                            onChange={handleOrderByChange}
                        >
                            {properties.map((p) =>
                                <MenuItem key={p.id} value={p.id}>{p.label}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                    <ToggleButtonGroup
                        color="primary"
                        value={order}
                        exclusive
                        onChange={handleOrderChange}
                    >
                        <ToggleButton value="desc"><Iconify sx={{height: 14, width: 'auto'}}
                                                            icon="cil:sort-ascending"/></ToggleButton>
                        <ToggleButton value="asc"><Iconify sx={{height: 14, width: 'auto'}}
                                                           icon="cil:sort-descending"/></ToggleButton>
                    </ToggleButtonGroup>
                </Stack>

            </Stack>
        </Card>
    )
}