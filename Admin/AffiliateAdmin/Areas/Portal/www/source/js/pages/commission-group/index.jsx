import React, { PureComponent } from 'react';
import { Provider } from 'react-redux';

import reducer from './reducers';
import AppContainer from './components/AppContainer';
import { configureStore } from '../../modules/store';
import { post } from '../../modules/ajaxRequest';

export default class App extends PureComponent {
    constructor(props) {
        super(props);
        this.state={
            productList:[],
            deductionList:[]
        }
    }

    componentDidMount = async () =>{
        const data = await this.fetchInitState();
        if(!!data)
        {
            this.setState({
                productList: data.ProductList,
                deductionList: data.DeductionList
            })
        }
    }

    fetchInitState = async () => {
        const url = 'Portal/CommissionGroup/FetchInitState';
        const response = await post(url);
        return response && response.status === 200 ? response.data : null;
    }

    render() {
        const store = configureStore(this.state, reducer);
        return (
            <Provider store={store}>
                <AppContainer />
            </Provider>
        );
    }
}