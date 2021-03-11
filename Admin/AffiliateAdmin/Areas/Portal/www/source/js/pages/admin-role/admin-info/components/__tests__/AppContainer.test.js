import React from 'react';
import { AppContainer } from '../AppContainer';
import { shallow } from 'enzyme';
import MyLink from '../../../../common/components/MyLink';

describe('affiliate info - components - AppContainer', () => {
    it('render correctly', () => {
        const result = shallow(<AppContainer isShowLoader={true} userName={''} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with has Username', () => {
        const result = shallow(<AppContainer isShowLoader={true} userName={'Abc'} />);

        expect(result).toMatchSnapshot();
    });

    it('redirectToAdminList - should be called', () => {
        const props = { isShowLoader: false, userName: 'Abc', redirectToAdminList: jest.fn() };
        const result = shallow(<AppContainer {...props} />);

        result.find(MyLink).props().onClick();

        expect(props.redirectToAdminList).toHaveBeenCalled();
    });
});