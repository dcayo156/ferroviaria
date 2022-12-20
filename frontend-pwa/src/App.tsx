import React from 'react';
import { CssBaseline } from '@mui/material';
import NavigationScroll from "./layout/NavigationScroll"
import Routes from './routes';
import { store } from './store'
import { Provider } from 'react-redux';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { ToastContainer } from 'react-toastify';
import { esES } from '@mui/x-data-grid';

const theme = createTheme({
  palette: {
    primary: {
      main: "#FCA101",
      light: "#FCEF01",
      dark: "#958E00"
    },
    secondary: {
      main: "#308200",
      contrastText: '#000',
    },
    grey: {
      "100": "#E1F1F3"
    }
  },
  typography: {
    h1: {
      fontSize: 40,
      fontWeight: 500,
      fontFamily: "'Satisfy', cursive"
    }
  },
  breakpoints: {
    values: {
      xs: 0,
      sm: 600,
      md: 900,
      lg: 1200,
      xl: 1800,
    }
  }
},
esES
);

function App() {
  return (
    <Provider store={store}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <ToastContainer position="bottom-right"
          autoClose={5000}
          hideProgressBar={false}
          newestOnTop={false}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover />
        <NavigationScroll>
          <Routes />
        </NavigationScroll>
      </ThemeProvider>
    </Provider>
  );
}

export default App;
