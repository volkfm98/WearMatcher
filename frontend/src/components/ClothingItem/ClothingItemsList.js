import ClothingItemListView from './ClothingItemListView';

const { Component } = require("react");


class ClothingItemList extends Component {
    constructor(props) {
        super(props);
        this.state = {
            items: [],
            error:  undefined,
            loaded: false
        };
    }

    async componentDidMount() {
        let _items;

        try {
            _items = await fetch(
                "https://localhost:44324/ClothingItems/Get/",
                {
                    method: 'GET',
                    mode: 'cors'
                }
            )
        } catch(err) {
            this.setState({
                error: err
            });
    
            console.log(err);

            return;
        }
        
        if (!_items.ok) {
            this.setState({
                error: _items.status,
                loaded: true
            })

            return;
        }

        this.setState({
            items: await _items.json(),
            loaded: true
        })
    }

    render() {
        console.log(this.props);

        if (this.state.error !== undefined) {
            return (
                <h1>We having troubles with getting our presous data: {String(this.state.error)}</h1>
            )
        }

        if (!this.state.loaded) {
            return <h1>Loading...</h1>
        }

        return (
            <ul>
                <h1>Sosalnya</h1>
                {this.state.items.map(item =>
                    <ClothingItemListView item={item} />
                )}
            </ul>
        );
    }
}

export default ClothingItemList;
