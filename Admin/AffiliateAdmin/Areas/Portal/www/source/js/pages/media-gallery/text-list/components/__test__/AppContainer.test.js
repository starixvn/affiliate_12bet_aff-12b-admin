import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';
import ControlButtons from '../../../common/components/ControlButtons';

describe('text list - components - AppContainer', () => {
    it('render correctly', () => {
        const result = shallow(<AppContainer isShowLoader={true} />);

        expect(result).toMatchSnapshot();
    });

    it('fetchData - should be called when call fetchData in ControlButons', () => {
        const param = {
            PageIndex: 1,
            PageSize: 20,
            category: "",
            currency: "",
            deviceType: 0,
            language: ""
        }
        const props = Object.assign({}, { isShowLoader: true,  fetchData: jest.fn(), param })
        const result = shallow(<AppContainer {...props} />);

        result.find(ControlButtons).props().fetchData();

        expect(props.fetchData).toHaveBeenCalledWith(param);
    });

});