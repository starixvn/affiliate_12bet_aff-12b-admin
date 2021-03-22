import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';
import ControlButtons from '../../../common/components/ControlButtons';

describe('news letter - components - AppContainer', () => {
    const defaultProps = {
        param: { pageIndex: 1, pageSize: 20 },
        isShowLoader: true,
        gridData: [],
        checkedNewsLetterPages: {},
        isModifiable: true
    };

    it('render correctly', () => {
        const result = shallow(<AppContainer {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has not permission', () => {
        const props = Object.assign({}, defaultProps, { isModifiable: false });
        const result = shallow(<AppContainer {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('fetchData - should be called when call fetchData in ControlButtons', () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() });
        const result = shallow(<AppContainer {...props} />);

        result.find(ControlButtons).props().fetchData();

        expect(props.fetchData).toHaveBeenCalledWith(defaultProps.param);
    });
});