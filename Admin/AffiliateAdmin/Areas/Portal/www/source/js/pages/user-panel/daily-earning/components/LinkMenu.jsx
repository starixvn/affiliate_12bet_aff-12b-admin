import React from 'react';
import { Breadcrumb, BreadcrumbItem } from 'reactstrap';

class LinkMenu extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    _renderBreadcrumbItem = (linkMenuList) => {
        const arrayItem = [];
        const lastIndex = linkMenuList.length - 1;

        for( let i = 0; i < lastIndex; i ++) {
            arrayItem.push(<BreadcrumbItem><a href="javascript:;" onClick={linkMenuList[i].handleClick}>{linkMenuList[i].title}</a></BreadcrumbItem>);
        }

        arrayItem.push(<BreadcrumbItem active>{linkMenuList[lastIndex].title}</BreadcrumbItem>)
        
        return arrayItem;
    }

    render() {
        const linkMenuList = this.props.linkMenuList || null;

        return linkMenuList && (
            <Breadcrumb>
            {
                this._renderBreadcrumbItem(linkMenuList)
            }
            </Breadcrumb>
        );
    }
}

export default (React.memo(LinkMenu));