import React from 'react';
import { shallow } from 'enzyme';

import { SubMenu } from '../SubMenu';

describe('admin role - common - components', () => {
    it('render correctly', () => {
        const result = shallow(<SubMenu isSelectedRolePermission={false} />);

        expect(result).toMatchSnapshot();
    })

    it('render correctly with isSelectedRolePermission', () => {
        const result = shallow(<SubMenu isSelectedRolePermission={true} />);

        expect(result).toMatchSnapshot();
    })
})