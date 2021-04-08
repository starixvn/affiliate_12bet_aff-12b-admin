import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';

describe('landing page list - components - AppContainer', () => {
    const defaultProps = {
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

    it('handleFetchData - should call fetchData', async() => {
        const props = Object.assign({}, {}, { isShowLoader: true, isModifiable: true, fetchData: jest.fn() })
        const result = shallow(<AppContainer {...props} />);

        result.instance().handleFetchData();

        expect(props.fetchData).toHaveBeenCalled();
    });
});