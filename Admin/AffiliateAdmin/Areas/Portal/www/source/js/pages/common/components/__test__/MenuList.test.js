import React from 'react'
import { shallow } from 'enzyme'
import MenuList from '../MenuList'

describe("common - Menu List component - ", () => {
    it("render correctly", () => {
        const props = {
            img: 'Areas/Portal/www/dist/imgs/i_media1.png',
            menus: [
                { label: 'test', href: '/test' }
            ]
        }

        const wrapper = shallow(<MenuList {...props} />)

        expect(wrapper).toMatchSnapshot();
    })
})