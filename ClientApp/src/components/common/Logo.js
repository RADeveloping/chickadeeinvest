import PropTypes from 'prop-types';
import {Link as RouterLink} from 'react-router-dom';
// @mui
import {useTheme} from '@mui/material/styles';
import {Box} from '@mui/material';

// ----------------------------------------------------------------------

Logo.propTypes = {
    disabledLink: PropTypes.bool,
    sx: PropTypes.object,
};
/**
 * Chickadee logo.
 * @param disabledLink Disables RouterLink.
 * @param dark For use on light backgrounds.
 * @param sx Additional styles.
 * @returns {JSX.Element}
 * @constructor
 */
export default function Logo({disabledLink = false, dark = false, sx}) {
    const theme = useTheme();

    const PRIMARY_LIGHT = theme.palette.primary.light;

    const PRIMARY_MAIN = theme.palette.primary.main;

    const PRIMARY_DARK = theme.palette.primary.dark;

    const logo = (
        <Box component="img" src={!dark ? "/images/logo4.svg" : "/images/logo.svg"}
             sx={{width: 115, height: 'auto', ...sx}}/>
    );

    if (disabledLink) {
        return <>{logo}</>;
    }

    return <RouterLink to="/">{logo}</RouterLink>;
}
