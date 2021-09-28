import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';
import * as ajaxRequest from '../../../../../modules/ajaxRequest';

describe('affiliate info - components - AppContainer', () => {
    const defaultProps = {
        isShowLoader: false,
        commGroupList: {},
        languageList: {},
        setLanguageList: jest.fn(),
        setCommGroupList: jest.fn()
    }
    it('render correctly', () => {
        const expectedResponse = { data: { CommGroupList: [], LanguageList: [] }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const result = shallow(<AppContainer {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with response error', () => {
        ajaxRequest.post = jest.fn();

        const result = shallow(<AppContainer {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('componentDidUpdate - with empty object', () => {
        const result = shallow(<AppContainer {...defaultProps} />);
        const instance = result.instance();
        ajaxRequest.post = jest.fn();

        instance.componentDidUpdate(defaultProps)

        expect(ajaxRequest.post).toHaveBeenCalled();
    });

    it('componentDidUpdate - with not empty object', () => {
        const props = { ...defaultProps, languageList: { abc: 123 }, commGroupList: { 123: 'abc' } }
        const result = shallow(<AppContainer {...props} />);
        const instance = result.instance();
        ajaxRequest.post = jest.fn();

        instance.componentDidUpdate(props)

        expect(ajaxRequest.post).not.toHaveBeenCalled();
    });
});