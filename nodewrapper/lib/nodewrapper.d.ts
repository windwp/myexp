declare module 'nodewrapper' {
	 class Greeter {
	    greeting: string;
	    constructor(message: string);
	    greet(): string;
	}
	export = Greeter;

}
