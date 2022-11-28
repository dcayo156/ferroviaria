import * as React from 'react';
import { Box, Button, Grid, TextField, MenuItem, IconButton, Menu } from "@mui/material";
import ArrowDownwardIcon from '@mui/icons-material/ArrowDownward';
import './search.css'

interface PlaceSearchProps {
    isLoaded: boolean
    map: any
    changeCenter: (cent: google.maps.LatLng | google.maps.LatLngLiteral | undefined, loc: string) => void
}

const PlaceSearch: React.FunctionComponent<PlaceSearchProps> = ({ isLoaded, map, changeCenter}) => {
    const [localidad, setLocalidad] = React.useState<string>("");
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const [predictions, setPredictions] = React.useState<google.maps.GeocoderResult[] | null>(null);
    const open = Boolean(anchorEl);
    const inputref = React.useRef<HTMLDivElement>(null);
    const handleClose = (index: number, value: string) => {
        if (index !== -1) {
            setLocalidad(value)
            changeCenter(predictions![index].geometry?.location, value)
        }
        setLocalidad(value)
        setAnchorEl(null);
    };
    const onOpenMenu = (event: React.MouseEvent<HTMLButtonElement>) => {
        const d: HTMLElement = inputref.current?.childNodes[1] as HTMLElement;
        setAnchorEl(d);
    }
    const onPlaceSearch = (event: React.MouseEvent<HTMLButtonElement>) => {
        if (isLoaded && map != null) {
            const geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': localidad }, function (results, status) {
                if (status == 'OK') {
                    console.log(results);
                    setPredictions(results)

                } else {
                    setPredictions(null)
                }
                const d: HTMLElement = inputref.current?.childNodes[1] as HTMLElement;
                setAnchorEl(d);
            });
        }
    }

    return <div className='search'>
        <Box display="flex" >
            <Box flex={9}>
                <TextField
                    ref={inputref}
                    id="localidad"
                    size="small"
                    fullWidth
                    label="Buscar ubicacion"
                    style={{background:"white",borderRadius:"5px"}}
                    value={localidad}
                    onChange={(e) => { setLocalidad(e.target.value) }}
                    InputProps={{
                        endAdornment: <IconButton color="primary" aria-label="Buscar" onClick={onOpenMenu}>
                            <ArrowDownwardIcon />
                        </IconButton>
                    }}
                />
                <Menu
                    id="basic-menu"
                    anchorEl={anchorEl}
                    open={open}
                    onClose={() => handleClose(-1, localidad)}
                    MenuListProps={{
                        'aria-labelledby': 'basic-button',
                    }}
                >
                    {
                        predictions != null ?
                            predictions.map((prediction, index) => {
                                return <MenuItem key={index} onClick={() => handleClose(index, prediction.formatted_address)}>{prediction.formatted_address}</MenuItem>
                            })
                            :
                            <MenuItem onClick={() => handleClose(-1, localidad)}>no se encontro resultados</MenuItem>
                    }
                </Menu>
            </Box>
            <Box flex={3}>
                <Button onClick={onPlaceSearch} variant="contained">Buscar</Button>
            </Box>
        </Box>
    </div>
}

export default PlaceSearch;