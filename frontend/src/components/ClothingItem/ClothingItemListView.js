import { useRouteMatch } from "react-router-dom";

const { Component } = require("react");

class ClothingItemListView extends Component {
    constructor(props) {
        super(props);
    }
    
    render() {
        return (
            <li class="list_item">
                <img src={this.props.item.img} />
                <em>{this.props.item.name}</em>
                <div class="tag_container">
                    Tags go here
                </div>
            </li>

        );
    }
}

export default ClothingItemListView
