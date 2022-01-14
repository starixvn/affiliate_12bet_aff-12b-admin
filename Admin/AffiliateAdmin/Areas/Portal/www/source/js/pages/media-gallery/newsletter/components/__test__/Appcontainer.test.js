import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';

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

    it('componentDidMount - should call handleFetchData when gridData is null', async () => {
        const props = Object.assign({}, defaultProps, { gridData: null, fetchData: jest.fn() })
        const result = shallow(<AppContainer {...props} />);
        const instance = result.instance();
        instance.handleFetchData = jest.fn();

        instance.componentDidMount();

        expect(instance.handleFetchData).toHaveBeenCalled();
    });

    it('componentDidUpdate - should call handleFetchData when param updated', async () => {
        const props = Object.assign({}, defaultProps, { gridData: null, fetchData: jest.fn(), param: {} })
        const result = shallow(<AppContainer {...props} />);
        const instance = result.instance();
        instance.handleFetchData = jest.fn();

        instance.componentDidUpdate({});

        expect(instance.handleFetchData).toHaveBeenCalled();
    });

    it('handleFetchData - should call fetchData', async () => {
        const props = Object.assign({}, defaultProps, { fetchData: jest.fn() })
        const result = shallow(<AppContainer {...props} />);

        result.instance().handleFetchData();

        expect(props.fetchData).toHaveBeenCalled();
    });
});