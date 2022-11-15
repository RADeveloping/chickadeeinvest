import Divider from "@mui/material/Divider";
import {ListItemButton} from "@mui/material";
import ListItemText from "@mui/material/ListItemText";
import Typography from "@mui/material/Typography";
import * as React from "react";
import {Link} from "react-router-dom";

/**
 * Generic component to list items using primary, secondary, tertiary values.
 * These values are derived from the filter methods in utils/constants.js.
 * @param uri {(a:object)=>string} URI method from utils/constants.js.
 * @param items {[object]} The items to be displayed.
 * @returns {JSX.Element}
 * @constructor
 */
export default function ListItems({uri, items}) {
    return (<>
        {items && items.length > 0 && items.map((item, index) =>
            <React.Fragment key={"list" + index}>
                {index !== 0 && <Divider sx={{marginLeft: 3}} key={`${item.id}-dvd1`}/>}
                <ListItemButton key={`${item.id}-btn`} component={Link} to={`/dashboard/${uri(item)}`}
                                alignItems="flex-start"
                >
                    <ListItemText sx={{marginLeft: 1}} key={`${item.id}-txt`}
                                  primary={item.primary}
                                  secondary={
                                      <>
                                          <Typography key={`${item.id}--typ`}
                                                      sx={{display: 'inline'}}
                                                      component="span"
                                                      variant="body2"
                                                      color="text.primary"
                                          >
                                              {item.secondary}
                                          </Typography>
                                          {item.tertiary}
                                      </>
                                  }
                    />
                </ListItemButton>
                {index === items.length - 1 && <Divider sx={{marginLeft: 3}} key={`${item.id}-dvd`}/>}
            </React.Fragment>)
        }</>)
}