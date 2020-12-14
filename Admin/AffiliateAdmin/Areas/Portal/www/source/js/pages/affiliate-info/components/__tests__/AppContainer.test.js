import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';
import * as ajaxRequest from '../../../../modules/ajaxRequest';

describe('affiliate info - components - AppContainer', () => {
    it('render correctly', () => {
        const expectedResponse = { data: { CommGroupList: [], LanguageList: [] }, status: 200 };
        ajaxRequest.post = jest.fn(() => Promise.resolve(expectedResponse));

        const result = shallow(<AppContainer isShowLoader={true} setLanguageList={jest.fn()} setCommGroupList={jest.fn()} />);

        expect(result).toMatchSnapshot();
    });
});